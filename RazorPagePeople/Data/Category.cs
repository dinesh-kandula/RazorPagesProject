using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RazorPagePeople.Data
{

    public enum Colours
    {
        RED, GREEN, BLUE, YELLOW, BLACK, WHITE
    }

    public class Category
    {
        public int Id { get; set; }
        [DisplayName("Category Name")]
        public required string Name { get; set; }

        public required string Description { get; set; }

        [RangeYear(12, ErrorMessage = "Year must be within the past 12 years.")]
        public int Year { get; set; }

        public required Colours Colour { get; set; }

        public override string ToString()
        {
            return $"Id: {Id}, Name: {Name}, Description: {Description}, Year: {Year}, Colour: {Colour}";
        }
    }

    public class RangeYearAttribute : RangeAttribute
    {
        public RangeYearAttribute(int yearsAgo)
            : base(DateTime.Now.Year - yearsAgo, DateTime.Now.Year)
        {
        }
    }
}
