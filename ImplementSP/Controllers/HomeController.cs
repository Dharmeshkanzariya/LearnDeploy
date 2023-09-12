using ImplementSP.Data;
using ImplementSP.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Diagnostics;

namespace ImplementSP.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly StoreContext _dbContext;

        public HomeController(ILogger<HomeController> logger, StoreContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            var results = _dbContext
            .Set<Product>()
            .FromSqlRaw("EXEC AllGetProduct")
            .ToList();

            return View(results);
        }

        public IActionResult AddProduct()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddProduct(string ProductName,Decimal Price)
        {
            var addProduct = _dbContext.Database.ExecuteSqlRaw("EXEC AddProduct @p0, @p1", ProductName, Price);
            return RedirectToAction("Index","Home");
        }

        public IActionResult UpdateProduct(int Id)
        {
            var products = _dbContext.Products
    .FromSqlRaw("EXEC ProductByID @p0", Id)
    .AsEnumerable()
    .FirstOrDefault();

            return View(products);
        }

        [HttpPost]
        public IActionResult UpdateProduct(int Id, string ProductName, Decimal Price)
        {
            var updateProduct = _dbContext.Database.ExecuteSqlRaw("EXEC UpdateProduct @p0, @p1 , @p2",Id, ProductName, Price);
            return RedirectToAction("Index","Home");
        }

        public IActionResult DeleteProduct(int Id)
        {
            
            var DeleteProduct = _dbContext.Database.ExecuteSqlRaw("EXEC DeleteProduct @p0", Id);
            return RedirectToAction("Index","Home");    
        }


        public IActionResult Calculator()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Calculator(string Act, int Num1, int Num2)
        {
            var param1 = new SqlParameter("@input1", Num1);
            var param2 = new SqlParameter("@input2", Num2);

            // Execute the stored procedure
            var sum = _dbContext.Database
                     .ExecuteSqlRaw("EXEC GetSumOfNumbers @input1, @input2", param1, param2);






            ViewBag.result = sum;
            return PartialView("_Result");
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