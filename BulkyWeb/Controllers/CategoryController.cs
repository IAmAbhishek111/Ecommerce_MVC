using BulkyWeb.Data;
using BulkyWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BulkyWeb.Controllers
{
    public class CategoryController : Controller
    {
        // constructor called

        public readonly ApplicationDbContext _db;

        public CategoryController(ApplicationDbContext db) {
            _db = db;
        }
        /*This is the action method */
        public IActionResult Index()
        {
            List<Category> objCategoryList = _db.Categories.ToList();
            return View(objCategoryList); // it will show category list  in UI
        }

        public IActionResult Create() {
           return View();
        }
        [HttpPost]
        public IActionResult Create(Category obj)

        {
            if(obj.Name == obj.DisplayOrder.ToString()) {
                ModelState.AddModelError("name", "The display order and name cannot be exactly the same");

            }
        
            if (ModelState.IsValid) {
                _db.Categories.Add(obj);   // this line is telling us that we have to add the category object into category table



                // to execute the changes 
                _db.SaveChanges();
            return RedirectToAction("Index");
            }

            return View();
          

        }

    }
}
