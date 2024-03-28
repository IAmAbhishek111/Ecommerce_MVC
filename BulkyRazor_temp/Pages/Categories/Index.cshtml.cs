using BulkyRazor_temp.Data;
using BulkyRazor_temp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BulkyRazor_temp.Pages.Categories
{
    public class IndexModel : PageModel
    {
        public readonly ApplicationDbContext _db;
        public List<Category> CategoryList { get; set; }

        public IndexModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public void OnGet()
        {
            CategoryList = _db.Categories.ToList();

        }
    }
}
