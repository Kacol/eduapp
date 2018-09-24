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
    public class DetailsModel : PageModel
    {
        private readonly AdminPanelWebApp.Models.DatabaseContext _context;

        public DetailsModel(AdminPanelWebApp.Models.DatabaseContext context)
        {
            _context = context;
        }

        public Question Question { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Question = await _context.Questions.FirstOrDefaultAsync(m => m.ID == id);

            if (Question == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
