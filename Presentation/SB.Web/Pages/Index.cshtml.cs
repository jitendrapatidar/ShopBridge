using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SB.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }
    }
}
//public class LoginModel
//{
//    [Required]
//    [EmailAddress]
//    public string UserName { get; set; }

//    [Required]
//    [DataType(DataType.Password)]
//    public string Password { get; set; }

//    [Display(Name = "Remember me?")]
//    public bool RememberMe { get; set; }

//    // public string Role { get; set; }
//    // public string Token { get; set; }
//}