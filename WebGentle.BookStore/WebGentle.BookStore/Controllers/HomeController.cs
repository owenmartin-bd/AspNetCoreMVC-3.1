using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using WebGentle.BookStore.Models;

namespace WebGentle.BookStore.Controllers
{
    [Route("[controller]/[action]")]
    public class HomeController : Controller
    {
        [ViewData]
        public string CustomProperty { get; set;  }
        [ViewData]
        public string Title { get; set;  }
        [ViewData]
        public BookModel Book { get; set; }
        private readonly IConfiguration configuration;


        public HomeController(IConfiguration _configuration)
        {
            configuration = _configuration;
        }

        [Route("~/")]
        public ViewResult Index()
        {
            Title = "Home page from controller";
            CustomProperty = "Custom value";

            var result = configuration.GetValue<bool>("NewBookAlert:DisplayNewBookAlert");
            var bookName = configuration.GetValue<string>("NewBookAlert:BookName");
            
            //var result = configuration["AppName"];
            //var key1 = configuration["infoObj:key1"];
            //var key2 = configuration["infoObj:key2"];
            //var key3 = configuration["infoObj:key3:key3obj1"];

            return View();
        }

        [Route("about-us")]
        public ViewResult AboutUs()
        {
            Title = "About page from controller";
            return View();
        }

        public ViewResult ContactUs()
        {
            return View();
        }
    }
}
