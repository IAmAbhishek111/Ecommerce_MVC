using BulkyRazor_temp.Data;
using BulkyRazor_temp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BulkyRazor_temp.Pages.Categories
{
    [BindProperties]
    public class CreateModel : PageModel
    {
        public readonly ApplicationDbContext _db;

        public Category Category { get; set; }

        public CreateModel(ApplicationDbContext db)
        {
            _db = db;
        }
        public void OnGet()
        {
        }

        public IActionResult OnPost(Category obj)
        {
            _db.Categories.Add(Category);
            _db.SaveChanges();

            return RedirectToPage("Index");


        }


    }
}
