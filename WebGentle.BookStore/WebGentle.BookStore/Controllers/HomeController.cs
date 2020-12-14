using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using WebGentle.BookStore.Models;
using WebGentle.BookStore.Repository;
using WebGentle.BookStore.Service;

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
        private readonly NewBookAlertConfig _newBookAlertconfiguration;
        private readonly NewBookAlertConfig _thirdPartyBookconfiguration;
        private readonly IMessageRepository _messageRepository;
        private readonly IUserService _userService;

        public HomeController(IOptionsSnapshot<NewBookAlertConfig> newBookAlertconfiguration,
            IMessageRepository messageRepository, IUserService userService)
        {
            _newBookAlertconfiguration = newBookAlertconfiguration.Get("InternalBook");
            _thirdPartyBookconfiguration = newBookAlertconfiguration.Get("ThirdPartyBook");
            _messageRepository = messageRepository;
            _userService = userService;
        }

        [Route("~/")]
        public ViewResult Index()
        {
            var userId = _userService.GetUserId();
            var isLoggedIn = _userService.IsAuthenticated();

            bool isDisplay = _newBookAlertconfiguration.DisplayNewBookAlert;
            bool isDisplay2 = _newBookAlertconfiguration.DisplayNewBookAlert;

            //var value = _messageRepository.GetName();


            //Title = "Home page from controller";
            //CustomProperty = "Custom value";

            //var newBook = configuration.GetSection("NewBookAlert");

            //var result = newBook.GetValue<bool>("DisplayNewBookAlert");
            //var bookName = newBook.GetValue<string>("BookName");
            
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
