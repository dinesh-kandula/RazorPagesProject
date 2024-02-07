using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MoviesRazorPages.Pages
{

    public class IndexModel : PageModel
    {

        public string ownerName { get; set; } = "DinnuBunny";
        public string CurrentDateTime { get; set; }

        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            DateTime now = DateTime.Now;

            CurrentDateTime = now.ToString("yyyy-MM-dd HH:mm:ss");
        }
    }
}
