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
    public class AddInventoryModel : PageModel
    {
        [BindProperty]
        public Inventory Inventory { get; set; }
        public string BaseUri { get; set; }
        public IActionResult OnGet()
        {
            var UserInfo = HttpContext.Session.Get<UserMaster>("UserInfo");

            if (UserInfo != null)
            {
                BaseUri = AppSettings.Instance.Get<string>("AppSettings:ServiceBaseUri");
                Inventory = new Inventory();
            }
            else
            {
                return new RedirectToPageResult("/Index");
            }

            return Page();
        }
       // public IActionResult
        public IActionResult OnPost(Inventory Inventory)
        {
            CommonResult oReuslt = new CommonResult();
            Inventory.Id = 0;
            Inventory.OnDate = DateTime.Now;
            var ServiceBaseUri = AppSettings.Instance.Get<string>("AppSettings:ServiceBaseUri") + "Inventory/Insert";
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
                oReuslt = JsonConvert.DeserializeObject<CommonResult>(result); 
                if (oReuslt != null)
                {
                    string oInventory = Convert.ToString(oReuslt.Result);
                    if (Convert.ToInt32(oInventory) > 0)
                    {
                        //Redirect("/Admin");
                        return new RedirectToPageResult("/Admin/index");// RedirectToPage("Admin");
                    }

                }
            }
            return Page();
        }
   }
}
