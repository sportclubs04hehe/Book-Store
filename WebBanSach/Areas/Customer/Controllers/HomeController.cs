using Bukly.Models;
using Bulky.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace WebBanSach.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public HomeController(IUnitOfWork unit, IWebHostEnvironment webHost)
        {
            _unitOfWork = unit;
            _webHostEnvironment = webHost;
        }

        public IActionResult Index()
        {
            IEnumerable<Product> products = _unitOfWork.productRepository.GetAll(includeProperties:"Category");
            return View(products);
        }

        public IActionResult Details(int id)
        {
            Product products = _unitOfWork.productRepository.Get(p=> p.Id == id, includeProperties:"Category");
            if(products == null)
            {
                return NotFound();
            }
            return View(products);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}