using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RazorPagePeople.Data;
using RazorPagePeople.Services;
using System.Security.Claims;

namespace RazorPagePeople.Pages.Person
{
    [Authorize]
    public class PersonsListModel : PageModel
    {
        private readonly PersonDatabaseContext _context;
        private readonly IFileService _fileService;

        public PersonsListModel(PersonDatabaseContext context, IFileService fileService)
        {
            this._context = context;
            this._fileService = fileService;
        }

        public IEnumerable<Data.Person> People { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }

        public SelectList? HeroName { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? NameHero { get; set; }

        public IActionResult OnGet()
        {
            var persons = from m in _context.Persons 
                          select m;
            if (!string.IsNullOrEmpty(SearchString))
            {
                persons = persons.Where(s => s.Name.Contains(SearchString));
            }
            People = persons.ToList();
            return Page();
        }


        public async Task<IActionResult> OnPostDeleteAsync(int? id)
        {
            if (new string[] { "ADMIN", "SHIELD" }.Contains(User.FindFirstValue(ClaimTypes.Role)))
            {
                if (id == null)
                    return NotFound();

                var person = _context.Persons.Find(id);
                if (person == null)
                    return NotFound();

                try
                {
                    if (person.Profile != null)
                        _fileService.DeleteImage(person.Profile);
                    _context.Persons.Remove(person);
                    await _context.SaveChangesAsync();
                    TempData["success"] = person.Name + " is deleted  Successfully";
                }
                catch (Exception ex)
                {
                    TempData["error"] = "Error has occured";
                }

                return RedirectToPage();
            }

            TempData["error"] = "Delete Operation Failed: Please Contact ADMIN";
            return RedirectToPage();
        }
    }
}
