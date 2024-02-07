using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RazorPagePeople.Data;

namespace RazorPagePeople.Pages.Category
{
    [Authorize(Policy = "ADMIN")]
    public class CreateModel : PageModel
    {
        private readonly RazorPagePeople.Data.PersonDatabaseContext _context;

        public CreateModel(RazorPagePeople.Data.PersonDatabaseContext context)
        {
            _context = context;
        }

        public int presentYear;
        public int pastYear;

        public IActionResult OnGet()
        {
            presentYear = DateTime.Now.Year;
            pastYear = presentYear - 12;
            return Page();
        }

        [BindProperty]
        public Data.Category Category { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Categories.Add(Category);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
