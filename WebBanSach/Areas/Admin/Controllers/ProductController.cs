using Bukly.Models;
using Bulky.DataAccess.Repository;
using Bulky.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebBanSach.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductController(IUnitOfWork unit)
        {
            _unitOfWork = unit;
        }
        public IActionResult Index()
        {
            IEnumerable<Product> products = _unitOfWork.productRepository.GetAll();
        
            return View(products);
        }
        public IActionResult Create()
        {
            IEnumerable<SelectListItem> CategoryList = _unitOfWork.categoryRepository.GetAll()
            .Select(
                u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }
            );
            ViewBag.CategoryList = CategoryList;
            return View();
        }

        [HttpPost]
        public IActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.productRepository.Add(product);
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

            Product? product = _unitOfWork.productRepository.Get(u => u.Id == id);
            //Product product1= _db.Categories.Find(id);
            //Product product2= _db.Categories.Where(u => u.Id == id).FirstOrDefault();
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost]
        public IActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.productRepository.Update(product);
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
            Product product = _unitOfWork.productRepository.Get(u => u.Id == id);

            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            if (id == 0 || id == null)
            {
                return NotFound();
            }

            Product? product = _unitOfWork.productRepository.Get(u => u.Id == id);

            if (ModelState.IsValid)
            {
                _unitOfWork.productRepository.Remove(product);
                _unitOfWork.Save();
                TempData["success"] = "Delete record successfully!";
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
