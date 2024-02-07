using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPagePeople.Data;
using RazorPagePeople.Services;

namespace RazorPagePeople.Pages.Person
{
    [Authorize("ADMIN")]
    public class EditPersonModel : PageModel
    {
        private readonly PersonDatabaseContext _context;
        private readonly IFileService _fileService;

        public EditPersonModel(PersonDatabaseContext context, IFileService fileService)
        {
            _context = context;
            this._fileService = fileService;
        }
        [BindProperty]
        public Data.Person Person { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _context.Persons.FindAsync(id);
            if (person == null)
                return NotFound();

            Person = person;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                TempData["error"] = "ModelState is Invalid";
                return Page();

            }
            try
            {
                if (Person == null)
                    return NotFound();


                string? oldImagePath = Person.Profile;

                if(Person.ImageFile != null)
                {
                    var serviceResult = _fileService.SaveImage(Person.ImageFile);
                    if (serviceResult.Item1 == 1)
                    {
                        Person.Profile = serviceResult.Item2;
                    }
                }

                Person.Gender = (Data.Gender)Enum.ToObject(typeof(Data.Gender), Person.Gender);
                Person.City = (Data.City)Enum.ToObject(typeof(Data.City), Person.City);
                
                _context.Attach(Person).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                TempData["success"] = "Updated Successfully";

                if(!string.IsNullOrEmpty(oldImagePath) && oldImagePath != Person.Profile)
                    _fileService.DeleteImage(oldImagePath);
            }
            catch (Exception ex)
            {
                TempData["error"] = "Error has occured";
            }

            return RedirectToPage("PersonsList");
        }
    }
}
