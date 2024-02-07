using BCrypt.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPagePeople.Data;
using RazorPagePeople.Services;
using System.Diagnostics;
using static System.Collections.Specialized.BitVector32;
 
namespace RazorPagePeople.Pages.Person
{
    [Authorize(Roles = "ADMIN")]
    public class CreateModel : PageModel
    {
        private readonly PersonDatabaseContext _context;
        private readonly IFileService _fileService;

        public CreateModel(PersonDatabaseContext context, IFileService fileService)
        {
            this._context = context;
            this._fileService = fileService;
        }


        [BindProperty]
        public Data.Person Person { get; set; }
        public List<Gender> GenderOptions { get; private set; }

        public IActionResult OnGet()
        {
            GenderOptions = Enum.GetValues(typeof(Gender)).Cast<Gender>().ToList();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {

            if (!ModelState.IsValid)
            {
                return Page();
            }
            try
            {
                if(Person == null)
                    return NotFound();
                // Profile Picture Storage
                if(Person.ImageFile  != null)
                {
                    var serviceResult = _fileService.SaveImage(Person.ImageFile);
                    if(serviceResult.Item1 == 1)
                    {
                        Person.Profile = serviceResult.Item2;
                    }
                }

                // Password Hasing
                var passwordHash = BCrypt.Net.BCrypt.HashPassword(Person.Password);
                Person.Password = passwordHash;

                Person.Gender = (Data.Gender)Enum.ToObject(typeof(Data.Gender), Person.Gender);
                Person.City = (Data.City)Enum.ToObject(typeof(Data.City), Person.City);
                _context.Persons.Add(Person);
                await _context.SaveChangesAsync();
                TempData["success"] = "Saved Successfully";
            }
            catch (Exception ex)
            {
                TempData["error"] = "Error has occured";
            }

            return RedirectToPage("PersonsList");
        }
    }
}

