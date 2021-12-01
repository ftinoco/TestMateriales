using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestMaterialesAPP.Tools
{
    public interface IHttpClientService
    {
        Task<List<TType>> GetAsync<TType>(object key);
          
        Task<Result> PostAsync<TType, IDType>(TType data);

        Task<Result> UpdateAsync<TType>(object key, TType data);

        Task<Result> DeleteAsync(object key);
    }
}
