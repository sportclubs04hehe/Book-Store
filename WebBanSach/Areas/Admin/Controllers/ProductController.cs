using Bukly.Models;
using Bukly.Models.ViewModel;
using Bukly.Utility;
using Bulky.DataAccess.Repository;
using Bulky.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

namespace WebBanSach.Areas.Admin.Controllers
{
    [Area("Admin"),Authorize(Roles = SD.Role_Admin)]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(IUnitOfWork unit, IWebHostEnvironment webHost)
        {
            _unitOfWork = unit;
            _webHostEnvironment = webHost;
        }
        public IActionResult Index()
        {
            IEnumerable<Product> products = _unitOfWork.productRepository.GetAll(includeProperties:"Category");

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
                //Dòng mã này truy xuất đường dẫn gốc web của ứng dụng ASP.NET Core
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if(file != null)
                {
                    #region Giải thích mã
                    //Dòng này tạo một tên tệp duy nhất cho tệp đã tải lên bằng
                    //cách kết hợp một Guidchuỗi mới được tạo với phần mở rộng
                    //ban đầu của tệp thu được từ file.FileNamethuộc tính.
                    //Điều này được thực hiện để tránh ghi đè lên các tệp hiện có cùng tên.
                    #endregion
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string productPath = Path.Combine(wwwRootPath, @"images\products");

                    if(!string.IsNullOrEmpty(prd.Product.Image)) {
                        //xóa hình ảnh cũ 
                        var oldPathImage =
                            Path.Combine(wwwRootPath, prd.Product.Image.TrimStart('\\'));

                        if (System.IO.File.Exists(oldPathImage))
                        {
                            System.IO.File.Delete(oldPathImage);
                        }
                    }

                    #region Giải thích mã
                    //Dòng này tạo một FileStream đối tượng để ghi tệp đã tải lên
                    //vào đường dẫn tệp đích, cho FileMode.Createbiết tệp mới sẽ được tạo.
                    #endregion
                    using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    prd.Product.Image = @"\images\products\" + fileName;
                }

                if(prd.Product.Id == 0)
                {
                    _unitOfWork.productRepository.Add(prd.Product);
                    _unitOfWork.Save();
                    TempData["success"] = "New record created successfully!";
                }
                else
                {
                    _unitOfWork.productRepository.Update(prd.Product);
                    _unitOfWork.Save();
                    TempData["success"] = "Update record successfully!";
                }
               
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



        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            IEnumerable<Product> products = _unitOfWork.productRepository.GetAll(includeProperties: "Category");
            return Json(new { data = products});

        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var productDeleted = _unitOfWork.productRepository.Get(p => p.Id == id);
            if(productDeleted == null)
            {
                return Json(new { success= false, message = "Error while deleting!"});
            }

            var oldPathImage =
                          Path.Combine(_webHostEnvironment.WebRootPath,
                          productDeleted.Image.TrimStart('\\'));

            if (System.IO.File.Exists(oldPathImage))
            {
                System.IO.File.Delete(oldPathImage);
            }

            _unitOfWork.productRepository.Remove(productDeleted);
            _unitOfWork.Save();

            return Json(new { success = true, message = "Delete Success!" });

        }
        #endregion
    }
}
