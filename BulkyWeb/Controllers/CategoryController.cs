using Bulky.DataAcess.Data;
using Bulky.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BulkyWeb.Controllers
{
    public class CategoryController : Controller
    {

        public readonly ApplicationDbContext _db;

        // constructor called
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
                TempData["Success"] = "Category Created Successfully";

                return RedirectToAction("Index");
            }

            return View();
          

        }

        public IActionResult Edit(int? id )

        {
            if(id == null || id == 0) {
                return NotFound();

            }
            Category? categoryFromDb = _db.Categories.Find(id);
            /*Category? categoryFromDb1 = _db.Categories.FirstOrDefault(u => u.Id == id);
            Category? categoryFromDb2 = _db.Categories.Where(u => u.Id == id).FirstOrDefault();*/


            if (categoryFromDb == null)
            {
                return NotFound();

            }

            return View(categoryFromDb);
        }
        [HttpPost]
        public IActionResult Edit (Category obj)

        {
          /*  if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The display order and name cannot be exactly the same");

            }
*/
            if (ModelState.IsValid)
            {
                _db.Categories.Update(obj);   // this line is telling us that we have to update the category object into category table

                // to execute the changes 
                _db.SaveChanges();
                TempData["Success"] = "Category Updated Successfully";
                return RedirectToAction("Index");
            }

            return View();


        }


        public IActionResult Delete(int? id)

        {
            if (id == null || id == 0)
            {
                return NotFound();

            }
            Category? categoryFromDb = _db.Categories.Find(id);
            /*Category? categoryFromDb1 = _db.Categories.FirstOrDefault(u => u.Id == id);
            Category? categoryFromDb2 = _db.Categories.Where(u => u.Id == id).FirstOrDefault();*/


            if (categoryFromDb == null)
            {
                return NotFound();

            }

            return View(categoryFromDb);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id )

        {
            /*  if (obj.Name == obj.DisplayOrder.ToString())
              {
                  ModelState.AddModelError("name", "The display order and name cannot be exactly the same");

              }

            
  */
            Category obj = _db.Categories.Find(id);

            if(obj == null)
            {
                return NotFound();

            }
            _db.Categories.Remove(obj);
            TempData["Success"] = "Category Deleted Successfully";
            _db.SaveChanges();
            return RedirectToAction("Index");
           

      


        }

    }
}

