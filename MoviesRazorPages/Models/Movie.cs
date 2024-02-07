using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MoviesRazorPages.Models
{
    public class Movie
    {
        public required int Id {  get; set; }

        [DisplayName("Movie Name")]
        public string? Title { get; set; }

        [DataType(DataType.Date), DisplayName("Release Date")]
        public DateTime ReleaseDate { get; set; }

        public string? Genre { get; set; }

        public decimal Price { get; set; }
    }
}
