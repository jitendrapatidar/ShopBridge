using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using SB.Model;

namespace SB.Web.Pages.Admin
{
    [BindProperties]
    public class IndexModel : PageModel
    {
        
        [BindProperty]
        public List<Inventory> Inventory { get; set; }

        public string BaseUri { get; set; }
        public IActionResult OnGet()
        {

            var UserInfo= HttpContext.Session.Get<UserMaster>("UserInfo");

            if (UserInfo!=null)
            {
                BaseUri = @AppSettings.Instance.Get<string>("AppSettings:ServiceBaseUri");

                var ServiceBaseUri = AppSettings.Instance.Get<string>("AppSettings:ServiceBaseUri") + "Inventory/GetAll";
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(ServiceBaseUri);
                //httpWebRequest.ContentType = "application/json";
                httpWebRequest.Accept = "application/json";
                httpWebRequest.Method = "GET";
                httpWebRequest.UseDefaultCredentials = true;
                httpWebRequest.PreAuthenticate = true;
                httpWebRequest.Credentials = CredentialCache.DefaultCredentials;
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    var oResult = JsonConvert.DeserializeObject<CommonResult>(result); //new JavaScriptSerializer().Deserialize<Response>(result);

                    if (oResult != null)
                    {
                        //List<Inventory> Inventory
                        string uinfo = Convert.ToString(oResult.Result);
                        Inventory = JsonConvert.DeserializeObject<List<Inventory>>(uinfo);// (UserMaster)oReuslt.Result;
                    }

                }
            }
            else {
                return new RedirectToPageResult("/Index");
            }

            return Page();

        }
    }
}
