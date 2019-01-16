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


    public class IndexModel : PageModel
    {
        private readonly AdminPanelWebApp.Models.DatabaseContext _context;

        public IndexModel(AdminPanelWebApp.Models.DatabaseContext context)
        {
            _context = context;
        }

        public IList<Question> Question { get;set; }

        public async Task OnGetAsync()
        {
            Question = await _context.Questions.ToListAsync();//zapytanie do serwera sql (SELECT * FROM....)
        }
    }
}
