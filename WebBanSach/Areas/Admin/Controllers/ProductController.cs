using Bukly.Models;
using Bukly.Models.ViewModel;
using Bulky.DataAccess.Repository;
using Bulky.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

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
        public IActionResult Upsert(int? id)
        {

            //ViewBag.CategoryList = CategoryList;
            //ViewData["CategoryList"] = CategoryList;
            #region Giải thích mã
            //mã này tạo một ProductVM đối tượng, đặt CategoryList
            //thuộc tính của nó thành danh sách SelectListItem các
            //đối tượng được truy xuất từ ​​cơ sở dữ liệu và đặt Product
            //thuộc tính của nó thành một đối tượng mới Product. Điều
            //này có khả năng được sử dụng để điền vào mô hình chế độ
            //xem với dữ liệu để hiển thị trong chế độ xem trong ứng dụng ASP.NET Core.
            #endregion
            ProductVM productVM = new()
            {
                Product = new(),
                CategoryList = _unitOfWork.categoryRepository.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                })
            };

            if(id == 0 || id == null)
            {
                //create
                return View(productVM);
            }
            else
            {
                //update
                productVM.Product = _unitOfWork.productRepository.Get(p => p.Id == id);
                return View(productVM);
            }
            
        }

        [HttpPost]
        public IActionResult Upsert(ProductVM prd, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.productRepository.Add(prd.Product);
                _unitOfWork.Save();
                TempData["success"] = "New record created successfully!";
                return RedirectToAction("Index");
            }else
            {
                prd.CategoryList = _unitOfWork.categoryRepository.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                });

                return View();
            }


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
