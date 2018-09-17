using AdminPanelWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AdminPanelWebApp.Pages.Questions
{
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
            //Question = new Question();
        }

        public void OnPost() //dodawanie do DB
        { 
            if(!ModelState.IsValid)
            {
                return; // w przypadku dodania smieci w formularzu
            }
            _databaseContext.Questions.Add(Question); // dodajemy rekordy w pamiêci RAM

            _databaseContext.SaveChanges(); // zapisujemy zamiany w DB

        }
    }
}