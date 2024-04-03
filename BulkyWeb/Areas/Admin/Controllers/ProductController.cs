using Bulky.DataAccess.Repository.IRepository;
using Bulky.DataAcess.Data;
using Bulky.Models;
using Bulky.Models.ViewModels;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace BulkyWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {

        public readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;

        // constructor called
        public ProductController(IUnitOfWork unitOfWork , IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
          
            _webHostEnvironment = webHostEnvironment;
        }
        /*This is the action method */
        public IActionResult Index()
        {
            List<Product> objProductList = _unitOfWork.Product.GetAll().ToList();

            

            return View(objProductList); // it will show product list  in UI
        }

        public IActionResult Upsert(int? id)
        {
        //That way what will happen is each category object here will be converted into a select list item and it will have a text and a value.

        //And that is how we can convert a category to an IEnumerable of select list item using projection.

            /*IEnumerable<SelectListItem> CategoryList =*/

            // 1st CategoryList is key and 2nd CategoryList is Value

            /* ViewBag.CategoryList = CategoryList; */
            /*ViewData[nameof(CategoryList)] = CategoryList;*/

            ProductVM productVM = new()
            {
                CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()

                }),
                Product = new Product()

            };
            if(id == null || id == 0)
            {
                //create

                return View(productVM);

            }
            else
            {
                //update
                productVM.Product = _unitOfWork.Product.Get( u => u.Id == id);
                return View(productVM);

            }

        }
        [HttpPost]
        public IActionResult Upsert(ProductVM productVM , IFormFile? file
            )

        {

            if (ModelState.IsValid)
            {/*
                string wwwRootPath = _webHostEnvironment.WebRootPath;

                if(file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);    
                    string ProductPath = Path.Combine(wwwRootPath , @"images\product");

                    using (var filestream = new FileStream(Path.Combine(ProductPath , fileName), FileMode.Create))
                    {
                        file.CopyTo(filestream);



                    }
                    productVM.Product.ImageUrl = @"\images\product\"  + fileName;

                }
                 */

                _unitOfWork.Product.Add(productVM.Product);   // this line is telling us that we have to add the product object into product table

                // to execute the changes 
                _unitOfWork.Save();
                TempData["Success"] = "Product Created Successfully";
                return RedirectToAction("Index");
            }

            else
            {

                productVM.CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()

                });
               
                return View(productVM);

            }



        }
       /* public IActionResult Edit(int? id)

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


        }*/

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

//  NOTES RELATED TO VIEW BAG:

// 1. ViewBag transfers data from controller to view, and not vice versa.
//2. ViewBag is a dynamic property that takes advantage of the new dynamic features in C# 4.0.
//3. Any number of properties and values can be assigned to a ViewBag, and the ViewBag's life only exists during the current HTTP request. ViewBag values will be null if there is any redirection.
//4. ViewBag is actually a wrapper around ViewData



// Notes Related to ViewData:

//1. ViewData also transfers the data from Controller to View, and not vice-versa. And again, it is ideal for the situation where the temporary data is not in a model.

// 2. ViewData is derived from ViewDataDictionary, which is a dictionary type.

//3. ViewData value must be type casted before use.

//4. the ViewBags life only lasts during the current http request, and ViewData values will be null if redirection occurs, similar to ViewBag.
/*
5. But there is one more thing that you should be aware of.
ViewBag internally inserts data into the ViewBagDictionary. So the key of ViewData, and property of ViewBag must not match because both of them will go to the same entity.*/


//Notes Related to Temp Data:
/*
1.TempData can be used to store data between two consecutive requests.

2. TempData internally use Session to store the data. So think of it as a short lived session, which will only exist for one request.
3. Now, TempData value must also be type cast before use. So check for null values to avoid any runtime errors.

4. Finally, as you already know, TempData can be used to store only onetime message like error messages or validation message. And if you refresh the page after that, it will go away.
*/