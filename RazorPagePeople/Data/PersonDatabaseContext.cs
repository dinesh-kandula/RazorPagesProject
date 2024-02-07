using Microsoft.EntityFrameworkCore;

namespace RazorPagePeople.Data
{
    public class PersonDatabaseContext : DbContext
 
    {
        public PersonDatabaseContext(DbContextOptions<PersonDatabaseContext> options) 
            : base(options)
        {
        }

        public DbSet<Person> Persons { get; set;}

        public DbSet<Category> Categories { get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure enum properties to be stored as strings for Person Table
            modelBuilder
                .Entity<Person>()
                .Property(e => e.Gender)
                .HasConversion<string>();

            modelBuilder
                .Entity<Person>()
                .Property(e => e.City)
                .HasConversion<string>();

            modelBuilder
               .Entity<Person>()
               .Property(e => e.Role)
               .HasConversion<string>();

            modelBuilder
                .Entity<Person>()
                .HasIndex(e => new {e.EmailId, e.UserName})
                .IsUnique(true);

            // Configure enum properties to be stored as strings for Person Table
            modelBuilder
                .Entity<Category>()
                .Property(e => e.Colour)
                .HasConversion<string>();
        }
    }
}
