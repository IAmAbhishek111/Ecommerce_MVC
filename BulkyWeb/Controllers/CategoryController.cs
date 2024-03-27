using BulkyWeb.Data;
using BulkyWeb.Models;
using Microsoft.AspNetCore.Mvc;

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
    }
}
