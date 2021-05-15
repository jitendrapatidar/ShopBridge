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
    public class EditInventoryModel : PageModel
    {
        [BindProperty]
        public Inventory Inventory { get; set; }
        public bool Disabled { get; set; }
        public IActionResult OnGet()
        {
            var UserInfo = HttpContext.Session.Get<UserMaster>("UserInfo");

            if (UserInfo != null)
            {



                //T=v&id GetUserById(int Id)
                string Type = Convert.ToString(HttpContext.Request.Query["T"]);
                int id = Convert.ToInt32(HttpContext.Request.Query["id"]);
                if (id > 0)
                {

                    if (Type.Trim().ToLower() == "v".Trim().ToLower()) Disabled = true;
                    if (Type.Trim().ToLower() == "e".Trim().ToLower()) Disabled = false;

                    var ServiceBaseUri = AppSettings.Instance.Get<string>("AppSettings:ServiceBaseUri") + "Inventory/GetById/" + id;
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
                            Inventory = JsonConvert.DeserializeObject<Inventory>(uinfo);
                        }

                    }
                }
            }
            else
            {
                return new RedirectToPageResult("/Index");
            }
            return Page();
        }
        public IActionResult OnPost(Inventory Inventory)
        {
            Inventory.OnDate = DateTime.Now;
            var ServiceBaseUri = AppSettings.Instance.Get<string>("AppSettings:ServiceBaseUri") + "Inventory/Update";
            string data = JsonConvert.SerializeObject(Inventory);
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(ServiceBaseUri);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Accept = "application/json";
            httpWebRequest.Method = "POST";
            httpWebRequest.UseDefaultCredentials = true;
            httpWebRequest.PreAuthenticate = true;
            httpWebRequest.Credentials = CredentialCache.DefaultCredentials;
            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                streamWriter.Write(data);
                streamWriter.Flush();
                streamWriter.Close();
            }
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                var oReuslt = JsonConvert.DeserializeObject<CommonResult>(result);
                if (oReuslt != null)
                {
                    string oInventory = Convert.ToString(oReuslt.Result);
                    if (Convert.ToInt32(oInventory) > 0)
                    {

                        return new RedirectToPageResult("/Admin/index");
                    }

                }
            }
            return Page();
        }
    }
}
