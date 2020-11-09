using Microsoft.AspNetCore.Mvc;
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

        [Route("~/")]
        public ViewResult Index()
        {
            Title = "Home page from controller";

            CustomProperty = "Custom value";
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
