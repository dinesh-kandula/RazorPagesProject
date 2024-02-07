using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MoviesRazorPages.Models;

namespace MoviesRazorPages.Data
{
    public class MoviesRazorPagesContext : DbContext
    {
        public MoviesRazorPagesContext (DbContextOptions<MoviesRazorPagesContext> options)
            : base(options)
        {
        }

        public DbSet<MoviesRazorPages.Models.Movie> Movie { get; set; } = default!;
    }
}
