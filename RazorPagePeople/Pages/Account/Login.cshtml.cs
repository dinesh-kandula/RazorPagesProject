using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPagePeople.Data;
using Syncfusion.EJ2.Base;
using Syncfusion.EJ2.Linq;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace RazorPagePeople.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly PersonDatabaseContext _context;

        public LoginModel(PersonDatabaseContext context)
        {
            this._context = context;
        }

        [BindProperty]
        public Credential Credential { get; set; }

        public Data.Person person { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Page();
            }
            return RedirectToPage("/Person/PersonsList");
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            try
            {
                Data.Person? user;
                if (Credential.UserName.EndsWith(".com"))
                    user = _context.Persons.FirstOrDefault(e => e.EmailId == Credential.UserName);
                else
                    user = _context.Persons.FirstOrDefault(u => u.UserName == Credential.UserName);
                
                if (user != null)
                {
                    var result = BCrypt.Net.BCrypt.Verify(Credential.Password, user.Password);
                    if (result)
                    {
                        var Claims = new List<Claim>
                        {
                            new (ClaimTypes.Name, user.Name),
                            new (ClaimTypes.Role, user.Role.ToString()),
                            new ("UserName", user.UserName)
                        };

                        var identity = new ClaimsIdentity(Claims, "MyCookieAuth");
                        ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);

                        var authProperties = new AuthenticationProperties
                        {
                            IsPersistent = Credential.RemeberMe
                        };

                        await HttpContext.SignInAsync("MyCookieAuth", claimsPrincipal, authProperties);
                            
                        TempData["success"] = "Login Sucessfull";
                        return RedirectToPage("/Person/PersonsList");
                    }
                    else
                        TempData["error"] = "Incorrect Password";
                }
                else
                    TempData["error"] = "Invalid User Name";
            }
            catch (Exception ex)
            {
                TempData["error"] = "Error has occured";
                return Page();
            }

            return Page();
        }
    }

    public class Credential
    {
        [Required]
        [DisplayName("User Name")]
        public required string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public required string Password { get; set; }

        [DisplayName("Remeber Me")]
        public bool RemeberMe { get; set; }
    }
}
