using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using SB.Model;
using SB.Web.Models;
using System;
using System.IO;
using System.Net;
using System.Net.Http;

namespace SB.Web.Pages.Account
{

    [BindProperties]
    public class LoginModel : PageModel
    {//
        [BindProperty]
        public Login Login { get; set; }


        private static readonly HttpClient _Client = new HttpClient();
       // private static JavaScriptSerializer _Serializer = new JavaScriptSerializer();


        public void OnGet()
        {
            //login = new Login();
            //login.UserName = "";
            //login.Password = "";
            //login.RememberMe = false;
        }
        public IActionResult OnPost(Login Login)
        {
            UserMaster oUser = new UserMaster();
            CommonResult oReuslt = new CommonResult();
            oUser.UserName = Login.UserName;
            oUser.Password = Login.Password;
            var ServiceBaseUri = AppSettings.Instance.Get<string>("AppSettings:ServiceBaseUri") + "User/UserLogin";
            string data = JsonConvert.SerializeObject(oUser);
            
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
                oReuslt = JsonConvert.DeserializeObject<CommonResult>(result); //new JavaScriptSerializer().Deserialize<Response>(result);

                if (oReuslt != null)
                {
                    string uinfo = Convert.ToString(oReuslt.Result);
                    var Result = JsonConvert.DeserializeObject<UserMaster>(uinfo);// (UserMaster)oReuslt.Result;

                    if (Result.Id > 0)
                    {
                        HttpContext.Session.Set("UserInfo", oReuslt);
                        return new RedirectToPageResult("/Admin/index");
                    }
                 
                }
                
                
                
            }
            return Page();
            
        }

    }
}
