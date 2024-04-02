using Bulky.DataAccess.Repository.IRepository;
using Bulky.DataAcess.Data;
using Bulky.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BulkyWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {

        public readonly IUnitOfWork _unitOfWork;

        // constructor called
        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        /*This is the action method */
        public IActionResult Index()
        {
            List<Product> objProductList = _unitOfWork.Product.GetAll().ToList();
            return View(objProductList); // it will show category list  in UI
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Product obj)

        {

            if (ModelState.IsValid)
            {
                _unitOfWork.Product.Add(obj);   // this line is telling us that we have to add the category object into category table

                // to execute the changes 
                _unitOfWork.Save();
                TempData["Success"] = "Product Created Successfully";

                return RedirectToAction("Index");
            }

            return View();


        }
        public IActionResult Edit(int? id)

        {
            if (id == null || id == 0)
            {
                return NotFound();

            }
            Product? productFromDb = _unitOfWork.Product.Get(u => u.Id == id);
            /*Product? categoryFromDb1 = _db.Categories.FirstOrDefault(u => u.Id == id);
            Product? categoryFromDb2 = _db.Categories.Where(u => u.Id == id).FirstOrDefault();*/


            if (productFromDb == null)
            {
                return NotFound();

            }

            return View(productFromDb);
        }
        [HttpPost]
        public IActionResult Edit(Product obj)

        {
            /*  if (obj.Name == obj.DisplayOrder.ToString())
              {
                  ModelState.AddModelError("name", "The display order and name cannot be exactly the same");

              }
  */
            if (ModelState.IsValid)
            {
                _unitOfWork.Product.Update(obj);   // this line is telling us that we have to update the category object into category table

                // to execute the changes 
                _unitOfWork.Save();
                TempData["Success"] = "Product Updated Successfully";
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
            Product? categoryFromDb = _unitOfWork.Product.Get(u => u.Id == id);
            /*Product? categoryFromDb1 = _db.Categories.FirstOrDefault(u => u.Id == id);
            Product? categoryFromDb2 = _db.Categories.Where(u => u.Id == id).FirstOrDefault();*/


            if (categoryFromDb == null)
            {
                return NotFound();

            }

            return View(categoryFromDb);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)

        {
            /*  if (obj.Name == obj.DisplayOrder.ToString())
              {
                  ModelState.AddModelError("name", "The display order and name cannot be exactly the same");

              } */
            Product obj = _unitOfWork.Product.Get(u => u.Id == id);

            if (obj == null)
            {
                return NotFound();

            }
            _unitOfWork.Product.Delete(obj);
            TempData["Success"] = "Product Deleted Successfully";
            _unitOfWork.Save();
            return RedirectToAction("Index");

        }

    }
}

