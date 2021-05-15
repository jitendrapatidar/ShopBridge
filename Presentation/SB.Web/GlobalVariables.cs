using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace SB.Web
{
    public static class GlobalVariables
    {
        public static HttpClient WebApiClient = new HttpClient();

        static GlobalVariables()
        {
            //"https": null, //localhost:44321/User/GetAll
            var ServiceBaseUri = AppSettings.Instance.Get<string>("AppSettings:ServiceBaseUri");
            WebApiClient.Timeout = TimeSpan.FromMinutes(30);
            WebApiClient.BaseAddress = new Uri(ServiceBaseUri);//ConfigurationManager.AppSettings["ServiceBaseUri"]);
            WebApiClient.DefaultRequestHeaders.Clear();
            WebApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }


    
}
