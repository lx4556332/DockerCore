using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DockerCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace DockerCore.Controllers
{
    public class ProductController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _config;
        private readonly IProductRepository _repository;
        public string Message { get; set; }
        public List<Product> Products { get; set; }

        public ProductController(ILogger<HomeController> logger, IConfiguration config, IProductRepository repository)
        {
            _logger = logger;
            _config = config;
            _repository = repository;
        }

        public IActionResult Index()
        {
            _logger.LogInformation("测试内容",null);
            ViewData["message"] = _config["MESSAGE"]??"深入浅出Net Core 与 Docker";
            var model = _repository.Products.ToList();
            return View(model);
        }
    }
}
