using Bulky.DataAccess.Repository.IRepository;
using Bulky.DataAcess.Data;
using Bulky.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BulkyWeb.Controllers
{
    public class CategoryController : Controller
    {

        public readonly IUnitOfWork  _unitOfWork;

        // constructor called
        public CategoryController(IUnitOfWork unitOfWork) {
            _unitOfWork = unitOfWork;
        }
        /*This is the action method */
        public IActionResult Index()
        {
            List<Category> objCategoryList = _unitOfWork.Category.GetAll().ToList();
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
                _unitOfWork.Category.Add(obj);   // this line is telling us that we have to add the category object into category table

                // to execute the changes 
                _unitOfWork.Save();
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
            Category? categoryFromDb = _unitOfWork.Category.Get(u => u.Id == id);
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
                _unitOfWork.Category.update(obj);   // this line is telling us that we have to update the category object into category table

                // to execute the changes 
                _unitOfWork.Save();
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
            Category? categoryFromDb = _unitOfWork.Category.Get(u => u.Id == id); 
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
            Category obj = _unitOfWork.Category.Get(u => u.Id == id);

            if (obj == null)
            {
                return NotFound();

            }
            _unitOfWork.Category.Delete(obj);
            TempData["Success"] = "Category Deleted Successfully";
            _unitOfWork.Save();
            return RedirectToAction("Index");
           

      


        }

    }
}

