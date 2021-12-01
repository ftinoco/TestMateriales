using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace TestMaterialesAPP.Tools
{
    public class HttpClientService : IHttpClientService
    {
        private readonly HttpClient _client;

        private readonly JsonSerializerOptions options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true,
        };

        public string EndPointUrl { private get; set; }

        public HttpClientService(HttpClient client)
        {
            _client = client;
        }

        public async Task<Result> DeleteAsync(object key)
        {
            if (string.IsNullOrWhiteSpace(EndPointUrl))
                throw new Exception("Url nulo");

            var httpResponse = await _client.DeleteAsync($"{EndPointUrl}/{key}");

            var result = JsonSerializer.Deserialize<Result>(await httpResponse.Content.ReadAsStringAsync(), options);

            return result;
        }

        public async Task<List<TType>> GetAsync<TType>(object parameters = null)
        {
            if (string.IsNullOrWhiteSpace(EndPointUrl))
                throw new Exception("Url nulo");

            var httpResponse = await _client.GetAsync($"{EndPointUrl}{ToUrl(parameters)}");

            if (!httpResponse.IsSuccessStatusCode)
                throw new Exception("Error al realizar la petición");

            var content = await httpResponse.Content.ReadAsStringAsync();
            var data = JsonSerializer.Deserialize<List<TType>>(content, options);

            return data;
        }
        public async Task<T> GetAllById<T>(int id)
        {
            var httpResponse = await _client.GetAsync($"{EndPointUrl}/{id}");

            if (!httpResponse.IsSuccessStatusCode)
                throw new Exception("Error al realizar la petición");

            var content = await httpResponse.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<T>(content, options);

            return result;
        }

        public async Task<Result> PostAsync<TType, IDType>(TType data)
        {
            Result result = new Result();

            if (string.IsNullOrWhiteSpace(EndPointUrl))
                throw new Exception("Url nulo");

            var content = JsonSerializer.Serialize(data);
            var httpResponse = await _client.PostAsync(EndPointUrl, new StringContent(content, Encoding.Default, "application/json"));

            if (!httpResponse.IsSuccessStatusCode)
            {
                try
                {
                    result = JsonSerializer.Deserialize<Result>(
                            await httpResponse.Content.ReadAsStringAsync(), options);
                }
                catch (Exception)
                {
                    result = JsonSerializer.Deserialize<Result>(
                            await httpResponse.Content.ReadAsStringAsync(), options);
                }
                return result;
            }

            result = JsonSerializer.Deserialize<Result>(await httpResponse.Content.ReadAsStringAsync(), options);
            return result;
        }

        public async Task<Result> UpdateAsync<TType>(object key, TType data)
        {
            Result result = new Result();

            if (string.IsNullOrWhiteSpace(EndPointUrl))
                throw new Exception("Endpoint url null");

            var content = JsonSerializer.Serialize(data);
            var httpResponse = await _client.PutAsync($"{EndPointUrl}/{key}", new StringContent(content, Encoding.Default, "application/json"));

            if (!httpResponse.IsSuccessStatusCode)
            {
                try
                {
                    result = JsonSerializer.Deserialize<Result>(
                            await httpResponse.Content.ReadAsStringAsync(), options);
                }
                catch (Exception)
                {
                    result = JsonSerializer.Deserialize<Result>(
                            await httpResponse.Content.ReadAsStringAsync(), options);
                }
                return result;
            }

            result = JsonSerializer.Deserialize<Result>(await httpResponse.Content.ReadAsStringAsync(), options);
            return result;
        }

        private string ToUrl(object queryStringParameters)
        {
            string url = "?"; 
            if (queryStringParameters == null) return string.Empty;

            Type type = queryStringParameters.GetType();
            var properties = type.GetProperties().ToList().ToArray();

            for (int i = 0; i <= properties.Length - 1; i++)
                url += $"{properties[i].Name}={properties[i].GetValue(queryStringParameters)}&";

            return url;
        }
    }
}
