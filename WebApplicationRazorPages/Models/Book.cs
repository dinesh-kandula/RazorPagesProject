using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApplicationRazorPages.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }

        [DisplayName("Book Name")]
        public required string  BookName { get; set; }
        public required int Price { get; set; }
        public string? Author { get; set; }
        public int Year { get; set; }
    }
}
