using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPagePeople.Data;

namespace RazorPagePeople.Pages.Category
{
    public class DetailsModel : PageModel
    {
        private readonly RazorPagePeople.Data.PersonDatabaseContext _context;

        public DetailsModel(RazorPagePeople.Data.PersonDatabaseContext context)
        {
            _context = context;
        }

        public Data.Category Category { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories.FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            else
            {
                Category = category;
            }
            return Page();
        }
    }
}
