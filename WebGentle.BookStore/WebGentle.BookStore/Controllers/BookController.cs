﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebGentle.BookStore.Models;
using WebGentle.BookStore.Repository;

namespace WebGentle.BookStore.Controllers
{
    [Route("[controller]/[action]")]
    public class BookController : Controller
    {
        private readonly IBookRepository _bookRepository = null;
        private readonly ILanguageRepository _languageRepository = null;
        private readonly IWebHostEnvironment _webHostEnvironment = null;

        public BookController(IBookRepository bookRepository, 
            ILanguageRepository languageRepository,
            IWebHostEnvironment webHostEnvironment)
        {
            _bookRepository = bookRepository;
            _languageRepository = languageRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        [Route("~/all-books")]
        public async Task<ViewResult> GetAllBooks()
        {
            var data = await _bookRepository.GetAllBooks();
            return View(data);
        }

        [Route("~/book-details/{id:int:min(1)}", Name = "bookDetailsRoute")]
        public async Task<ViewResult> GetBook(int id)
        {
            var data = await _bookRepository.GetBookById(id);
            return View(data);
        }

        public List<BookModel> SearchBooks(string bookName, string authorName)
        {
            return _bookRepository.SearchBook(bookName, authorName);
        }

        public async Task<ViewResult> AddNewBook(bool isSuccess = false, int bookId = 0)
        {
            //var model = new BookModel() { Language = "English" };

            //ViewBag.Language = new SelectList(GetLanguage(), "Id", "Text");

            //ViewBag.Language = GetLanguage().Select(x => new SelectListItem() { 
            //    Text = x.Text,
            //    Value = x.Id.ToString()
            //}).ToList();

            //var group1 = new SelectListGroup() { Name = "Group 1" };
            //var group2 = new SelectListGroup() { Name = "Group 2", Disabled = true };
            //var group3 = new SelectListGroup() { Name = "Group 3" };

            //ViewBag.Language = new List<SelectListItem>()
            //{
            //    new SelectListItem() { Text = "Hindi", Value = "1" },
            //    new SelectListItem() { Text = "English", Value = "2" },
            //    new SelectListItem() { Text = "Dutch", Value = "3" },
            //    new SelectListItem() { Text = "Tamil", Value = "4" },
            //    new SelectListItem() { Text = "Urdu", Value = "5" },
            //    new SelectListItem() { Text = "Chinese", Value = "6" },
            //};

            //ViewBag.Language = 
            //    new SelectList(await _languageRepository.GetLanguages(), "Id", "Name");

            ViewBag.IsSuccess = isSuccess;
            ViewBag.BookId = bookId;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddNewBook(BookModel bookModel)
        {
            if (ModelState.IsValid)
            {
                // upload image to server
                if(bookModel.CoverPhoto != null)
                {
                    string folder = "books/cover/";
                    bookModel.CoverImageUrl = 
                        await UploadFile(folder, bookModel.CoverPhoto);
                }
                if (bookModel.GalleryFiles != null)
                {
                    string folder = "books/gallery/";
                    bookModel.Gallery = new List<GalleryModel>();
                    foreach (var file in bookModel.GalleryFiles)
                    {
                        var gallery = new GalleryModel() 
                        {
                            Name = file.FileName,
                            URL = await UploadFile(folder, file)
                        };
                        bookModel.Gallery.Add(gallery);
                    }
                }

                if (bookModel.BookPdf != null)
                {
                    string folder = "books/pdf/";
                    bookModel.BookPdfUrl = await UploadFile(folder, bookModel.BookPdf);
                }

                int id = await _bookRepository.AddNewBook(bookModel);
                if (id > 0)
                {
                    return RedirectToAction(nameof(AddNewBook), new { isSuccess = true, bookId = id });
                }
            }
            //var group1 = new SelectListGroup() { Name = "Group 1" };
            //var group2 = new SelectListGroup() { Name = "Group 2", Disabled = true };
            //var group3 = new SelectListGroup() { Name = "Group 3" };

            //ViewBag.Language = new List<SelectListItem>()
            //{
            //    new SelectListItem() { Text = "Hindi", Value = "1" },
            //    new SelectListItem() { Text = "English", Value = "2" },
            //    new SelectListItem() { Text = "Dutch", Value = "3" },
            //    new SelectListItem() { Text = "Tamil", Value = "4" },
            //    new SelectListItem() { Text = "Urdu", Value = "5" },
            //    new SelectListItem() { Text = "Chinese", Value = "6" },
            //};
            //ViewBag.IsSuccess = false;
            //ViewBag.BookId = 0;
            //ModelState.AddModelError("", "This is my custom error message");
            //ViewBag.Language =
            //    new SelectList(await _languageRepository.GetLanguages(), "Id", "Name");
            return View(); 
        }

        private async Task<string> UploadFile(string folderPath, IFormFile file)
        {
            
            folderPath += $"{Guid.NewGuid()}_{file.FileName}";
            //bookModel.CoverImageUrl = $"/{folder}";

            string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folderPath); ;

            await file.CopyToAsync(new FileStream(serverFolder, FileMode.Create));

            return $"/{folderPath}";
        }

    }
}
