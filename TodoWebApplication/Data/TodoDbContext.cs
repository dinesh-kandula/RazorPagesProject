using Microsoft.EntityFrameworkCore;
using TodoWebApplication.Model;

namespace TodoWebApplication.Data
{
    public class TodoDbContext : DbContext
    {

        public TodoDbContext(DbContextOptions<TodoDbContext> options) : base(options)
        {
            
        }

        public virtual DbSet<Todo> Todos { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Credential> Credentials { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .Property(u => u.Gender)
                .HasConversion<string>();

            modelBuilder.Entity<Todo>()
                .Property(t => t.Status)
                .HasConversion<string>();

            modelBuilder.Entity<Todo>()
                .Property(t => t.Priority)
                .HasConversion<string>();

            modelBuilder.Entity<Credential>()
                .HasIndex(c => new { c.Username, c.EmailId })
                .IsUnique(true);
        }


    }
}
