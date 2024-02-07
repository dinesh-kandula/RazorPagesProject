using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPagePeople.Data;

namespace RazorPagePeople.Pages.Category
{
    [Authorize(Policy = "HERO ADMIN")]
    public class IndexModel : PageModel
    {
        private readonly RazorPagePeople.Data.PersonDatabaseContext _context;

        public IndexModel(RazorPagePeople.Data.PersonDatabaseContext context)
        {
            _context = context;
        }

        public IList<Data.Category> Category { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Category = await _context.Categories.ToListAsync();
        }
    }
}
