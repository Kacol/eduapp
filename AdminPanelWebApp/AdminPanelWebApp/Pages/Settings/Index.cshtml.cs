using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;

namespace AdminPanelWebApp.Pages.Settings
{
    public class IndexModel : PageModel
    {
        private readonly IConfiguration _configuration;

        [BindProperty] 
        public int Timeout { get; set; }

        public IndexModel(IConfiguration configuration)
        {
            _configuration = configuration;            
        }

        public void OnGet()
        {
            Timeout = _configuration.GetValue<int>("AdminPanelSettings:Timeout");
        }
    }
}