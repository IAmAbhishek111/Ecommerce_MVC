using Bulky.DataAccess.Repository.IRepository;
using Bulky.DataAcess.Data;
using Bulky.Models;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;

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

            //That way what will happen is each category object here will be converted into a select list item and it will have a text and a value.

            //And that is how we can convert a category to an IEnumerable of select list item using projection.

            IEnumerable<SelectListItem> CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()

            }); ;   
            return View(objProductList); // it will show product list  in UI
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
                _unitOfWork.Product.Add(obj);   // this line is telling us that we have to add the product object into product table

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
      

            if (productFromDb == null)
            {
                return NotFound();

            }

            return View(productFromDb);
        }
        [HttpPost]
        public IActionResult Edit(Product obj)

        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Product.Update(obj);   // this line is telling us that we have to update the Product object into category table

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
   

            if (categoryFromDb == null)
            {
                return NotFound();

            }

            return View(categoryFromDb);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)

        {
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

