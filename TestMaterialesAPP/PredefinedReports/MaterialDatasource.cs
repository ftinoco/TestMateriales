using DevExpress.DataAccess.ObjectBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TestMaterialesAPP.Tools;
using TestMaterialesAPP.ViewModels;

namespace TestMaterialesAPP.PredefinedReports
{
    [DisplayName("MaterialDatasource")]
    [HighlightedClass]
    public class MaterialDatasource
    {
        public MaterialDatasource()
        {

        }

        [HighlightedMember]
        public IEnumerable<MaterialViewModel> GetMaterialList()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44365/api/");
            HttpClientService httpClientService = new HttpClientService(client);
            httpClientService.EndPointUrl = "Material";
            return httpClientService.GetAsync<MaterialViewModel>().Result;
        }
    }
}
