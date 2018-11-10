using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Required]
        [Display(Name = "Czas w sekundach")]
        [Range(300,7200, ErrorMessage = "Nieprawidlowa wartosc")]
        public int Timeout { get; set; }

        [BindProperty]
        [Required]
        [Display(Name = "Czas kary w sekundach")]
        [Range(60, 3600, ErrorMessage = "Nieprawidlowa wartosc")]
        public int WrongAnswerTimout { get; set; }
        //TODO: walidacja

        public IndexModel(IConfiguration configuration)
        {
            _configuration = configuration;            
        }

        public void OnGet()
        {
            Timeout = _configuration.GetValue<int>("AdminPanelSettings:Timeout");
            WrongAnswerTimout = _configuration.GetValue<int>("AdminPanelSettings:WrongAnswerTimout");
        }

        public ActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            _configuration["AdminPanelSettings:Timeout"]  = Timeout.ToString();
            _configuration["AdminPanelSettings:WrongAnswerTimout"] = WrongAnswerTimout.ToString();

            return RedirectToPage("Index");
        }
    }
}