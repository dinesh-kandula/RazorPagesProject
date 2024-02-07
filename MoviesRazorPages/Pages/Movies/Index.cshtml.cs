using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MoviesRazorPages.Data;
using MoviesRazorPages.Models;

namespace MoviesRazorPages.Pages.Movies
{
    public class IndexModel : PageModel
    {
        private readonly MoviesRazorPages.Data.MoviesRazorPagesContext _context;

        public IndexModel(MoviesRazorPages.Data.MoviesRazorPagesContext context)
        {
            _context = context;
        }

        public IList<Movie> Movie { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Movie = await _context.Movie.ToListAsync();
        }
    }
}
