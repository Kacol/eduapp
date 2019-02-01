using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AdminPanelWebApp.Models;
using Microsoft.AspNetCore.Authorization;

namespace AdminPanelWebApp.Pages.Questions
{

    [Authorize(Roles = "Administrator")]
    public class EditModel : PageModel
    {
        private readonly AdminPanelWebApp.Models.DatabaseContext _context;

        public EditModel(AdminPanelWebApp.Models.DatabaseContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Question Question { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Question = await _context.Questions.FirstOrDefaultAsync(m => m.ID == id); //context w tym wypadku do pobierania

            if (Question == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync() //edycja rekordu
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Question).State = EntityState.Modified; //tu do modyfikacji(informujemy context o modyfikacji)

            try
            {
                await _context.SaveChangesAsync();// tu nastepuje proba zapisu w DB
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QuestionExists(Question.ID)) // w wypadku gdy ktos usunal rekord podczas naszej edycji
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool QuestionExists(int id)
        {
            return _context.Questions.Any(e => e.ID == id);
        }
    }
}
