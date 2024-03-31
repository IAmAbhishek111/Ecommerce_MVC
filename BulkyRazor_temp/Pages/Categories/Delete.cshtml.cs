using BulkyRazor_temp.Data;
using BulkyRazor_temp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BulkyRazor_temp.Pages.Categories
{
    [BindProperties]

    public class DeleteModel : PageModel
    {
        

            public readonly ApplicationDbContext _db;

            public Category Category { get; set; }

            public DeleteModel(ApplicationDbContext db)
            {
                _db = db;
            }
            public void OnGet(int? id)
            {
                if (id != null && id != 0)
                {
                    Category = _db.Categories.Find(id);

                }
            }

            public IActionResult OnPost()
            {
            Category obj = _db.Categories.Find(Category.Id);

            if (obj == null)
            {
                return NotFound();

            }
            _db.Categories.Remove(obj);
            /* TempData["Success"] = "Category Deleted Successfully";*/
            TempData["Success"] = "Category Deleted Successfully";
            _db.SaveChanges();
            return RedirectToPage("Index");
            }
        }
}
