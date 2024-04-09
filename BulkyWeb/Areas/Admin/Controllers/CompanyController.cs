using Bulky.DataAccess.Repository.IRepository;
using Bulky.DataAccess.Data;
using Bulky.Models;
using Bulky.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
/*using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection.Metadata.Ecma335;
using System.Text;*/
using System.Data;


namespace BulkyWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CompanyController : Controller
    {

        public readonly IUnitOfWork _unitOfWork;
       

        // constructor called
        public CompanyController(IUnitOfWork unitOfWork )
        {
            _unitOfWork = unitOfWork;
          
        }
        /*This is the action method */
        public IActionResult Index()
        {
            List<Company> objCompanyList = _unitOfWork.Company.GetAll().ToList();

            

            return View(objCompanyList); // it will show Company list  in UI
        }

        public IActionResult Upsert(int? id)
        {

            if(id == null || id == 0)
            {
                //create

                return View(new Company());

            }
            else
            {
                //update
                Company companyObj = _unitOfWork.Company.Get( u => u.Id == id );
                return View(companyObj);

            }

        }
        [HttpPost]
        public IActionResult Upsert(Company companyObj )

        {

            if (ModelState.IsValid)
            {
               if(companyObj.Id == 0)
                {
                    _unitOfWork.Company.Add(companyObj);   // this line is telling us that we have to add the Company object into Company table
                }
                else
                { 
                    _unitOfWork.Company.update(companyObj);

                }                                 
               
                // to execute the changes 
                _unitOfWork.Save();
                TempData["Success"] = "Company Created Successfully";
                return RedirectToAction("Index");
            }

            else
            {               
                return View(companyObj);

            }

        }
       /* public IActionResult Edit(int? id)

        {
            if (id == null || id == 0)
            {
                return NotFound();

            }
            Company? CompanyFromDb = _unitOfWork.Company.Get(u => u.Id == id);
      

            if (CompanyFromDb == null)
            {
                return NotFound();

            }

            return View(CompanyFromDb);
        }
        [HttpPost]
        public IActionResult Edit(Company obj)

        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Company.Update(obj);   // this line is telling us that we have to update the Company object into category table

                // to execute the changes 
                _unitOfWork.Save();
                TempData["Success"] = "Company Updated Successfully";
                return RedirectToAction("Index");
            }

            return View();


        }*/

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll() {
            List<Company> objCompanyList = _unitOfWork.Company.GetAll().ToList();

            return Json(new {data = objCompanyList});   

        }
        
        [HttpDelete]
        public IActionResult Delete(int ? id)
        {
            var CompanyToBeDeleted = _unitOfWork.Company.Get(u => u.Id == id);
            
            if (CompanyToBeDeleted == null)
            {
                return Json(new { success = false , message = "Error While Deleting" });
            }

           

            _unitOfWork.Company.Delete(CompanyToBeDeleted);

            _unitOfWork.Save();

            List<Company> objCompanyList = _unitOfWork.Company.GetAll(includeProperties: "Category").ToList();

            return Json(new { success = true , message = "Delete Successful" });

        }
        #endregion

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