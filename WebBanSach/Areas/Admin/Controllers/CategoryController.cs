using Bukly.Models;
using Bukly.Utility;
using Bulky.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using WebBanSach.Data;

namespace WebBanSach.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize(Roles = SD.Role_Admin)]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            List<Category> objCategory = _unitOfWork.categoryRepository.GetAll().ToList();
            return View(objCategory);
        }
        
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category category)
        {
            if (category != null)
            {
                if (category.Name == category.DisplayOrder.ToString())
                {
                    ModelState.AddModelError("name", "The name of the category cannot match the display order!");
                }
            }
            if (ModelState.IsValid)
            {
                _unitOfWork.categoryRepository.Add(category);
                _unitOfWork.Save();
                TempData["success"] = "New record created successfully!";
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

            Category? category = _unitOfWork.categoryRepository.Get(u => u.Id == id);
            //Category category1= _db.Categories.Find(id);
            //Category category2= _db.Categories.Where(u => u.Id == id).FirstOrDefault();
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost]
        public IActionResult Edit(Category category)
        {
            if (category != null)
            {
                if (category.Name == category.DisplayOrder.ToString())
                {
                    ModelState.AddModelError("name", "The name of the category cannot match the display order!");
                }
            }
            if (ModelState.IsValid)
            {
                _unitOfWork.categoryRepository.Update(category);
                _unitOfWork.Save();
                TempData["success"] = "Edit record successfully!";
                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult Delete(int? id)
        {
            if (id == 0 || id == null)
            {
                return NotFound();
            }
            Category category = _unitOfWork.categoryRepository.Get(u => u.Id == id);

            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            if (id == 0 || id == null)
            {
                return NotFound();
            }

            Category? category = _unitOfWork.categoryRepository.Get(u => u.Id == id);

            if (ModelState.IsValid)
            {
                _unitOfWork.categoryRepository.Remove(category);
                _unitOfWork.Save();
                TempData["success"] = "Delete record successfully!";
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
