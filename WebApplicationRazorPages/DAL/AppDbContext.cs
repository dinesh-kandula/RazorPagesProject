using Microsoft.EntityFrameworkCore;
using WebApplicationRazorPages.Models;

namespace WebApplicationRazorPages.DAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public virtual DbSet<Book> Books { get; set; }
    }
}
