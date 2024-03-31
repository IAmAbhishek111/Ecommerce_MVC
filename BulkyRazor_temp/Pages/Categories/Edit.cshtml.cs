using BulkyRazor_temp.Data;
using BulkyRazor_temp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BulkyRazor_temp.Pages.Categories
{
    [BindProperties]
    public class EditModel : PageModel
    {
      

        public readonly ApplicationDbContext _db;

        public Category Category { get; set; }

        public EditModel(ApplicationDbContext db)
        {
            _db = db;
        }
        public void OnGet(int? id)
        {
            if(id != null && id != 0)
            {
                Category = _db.Categories.Find(id);

            }
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                _db.Categories.Update(Category);   // this line is telling us that we have to update the category object into category table

                // to execute the changes 
                _db.SaveChanges();
                TempData["Success"] = "Category Updated Successfully";
                return RedirectToPage("Index");
            }

            return Page();


         



        }
    }
}
