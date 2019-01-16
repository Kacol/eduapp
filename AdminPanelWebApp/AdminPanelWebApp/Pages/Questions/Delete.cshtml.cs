using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AdminPanelWebApp.Models;

namespace AdminPanelWebApp.Pages.Questions
{
    public class DeleteModel : PageModel
    {
        private readonly AdminPanelWebApp.Models.DatabaseContext _context;

        public DeleteModel(AdminPanelWebApp.Models.DatabaseContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Question Question { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id) //pobianie rekordu do usunięcia
        {
            if (id == null)
            {
                return NotFound();
            }

            Question = await _context.Questions.FirstOrDefaultAsync(m => m.ID == id); //odnosimy sie do odpowiedniej tabeli w context

            if (Question == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id) // usuwanie
        {
            if (id == null)
            {
                return NotFound();
            }

            Question = await _context.Questions.FindAsync(id);

            if (Question != null)
            {
                _context.Questions.Remove(Question);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
