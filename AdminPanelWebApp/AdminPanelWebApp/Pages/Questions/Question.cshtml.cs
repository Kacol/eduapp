using AdminPanelWebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AdminPanelWebApp.Pages.Questions
{
    [Authorize(Roles = "Administrator")]
    public class QuestionModel : PageModel
    {
        [BindProperty]
        public Question Question { get; set; }

        private DatabaseContext _databaseContext;
        public QuestionModel(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public void OnGet()
        {
            //Question = new Question();C:\Users\Michal\Source\Repos\eduapp\AdminPanelWebApp\AdminPanelWebApp\Pages\Questions\Delete.cshtml.cs
        }

        public ActionResult OnPost() //dodawanie do DB
        { 
            if(!ModelState.IsValid)
            {
                return Page(); // w przypadku dodania smieci w formularzu
            }
            _databaseContext.Questions.Add(Question); // dodajemy rekordy w pamiêci RAM

            _databaseContext.SaveChanges(); // zapisujemy zamiany w DB wysy³a do serwera sql

            return RedirectToPage("Index");
        }
    }
}