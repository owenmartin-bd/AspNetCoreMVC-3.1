using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebGentle.BookStore.Data;
using WebGentle.BookStore.Models;

namespace WebGentle.BookStore.Repository
{
    public class BookRepository
    {
        private readonly BookStoreContext _context;

        public BookRepository(BookStoreContext context)
        {
            _context = context;
        }

        public async Task<int> AddNewBook(BookModel model)
        {
            var newBook = new Books()
            {
                Author = model.Author,
                Description = model.Description,
                Title = model.Title,
                LanguageId = model.LanguageId,
                TotalPages = model.TotalPages ?? 0,
                CreatedOn = DateTime.UtcNow,
                UpdatedOn = DateTime.UtcNow,
                CoverImageUrl = model.CoverImageUrl,
                BookPdfUrl = model.BookPdfUrl
            };
            newBook.bookGallery = new List<BookGallery>();
            foreach (var file in model.Gallery)
            {
                newBook.bookGallery.Add(new BookGallery() 
                {
                    Name = file.Name,
                    URL = file.URL
                });
            }

            await _context.Books.AddAsync(newBook);
            await _context.SaveChangesAsync();

            return newBook.Id;
        }

        public async Task<List<BookModel>> GetAllBooks()
        {
            return await _context.Books.
                Select(book => new BookModel() 
                {
                    Author = book.Author,
                    Category = book.Category,
                    Description = book.Description,
                    Id = book.Id,
                    LanguageId = book.LanguageId,
                    Language = book.Language.Name,
                    Title = book.Title,
                    TotalPages = book.TotalPages,
                    CoverImageUrl = book.CoverImageUrl
                }).ToListAsync();
        }

        public async Task<BookModel> GetBookById(int id)
        {
            return await _context.Books.Where(x => x.Id == id)
                .Select(book => new BookModel()
                {
                    Author = book.Author,
                    Category = book.Category,
                    Description = book.Description,
                    Id = book.Id,
                    LanguageId = book.LanguageId,
                    Language = book.Language.Name,
                    Title = book.Title,
                    TotalPages = book.TotalPages,
                    CoverImageUrl = book.CoverImageUrl,
                    Gallery = book.bookGallery.Select(book => new GalleryModel
                    {
                        Id = book.Id,
                        Name = book.Name,
                        URL = book.URL
                    }).ToList(),
                    BookPdfUrl = book.BookPdfUrl
                }).FirstOrDefaultAsync();
        }

        public List<BookModel> SearchBook(string title, string authorName)
        {
            return null;
        }

        //private List<BookModel> DataSource()
        //{
        //    return new List<BookModel>()
        //    {
        //        new BookModel() { Id = 1, Title = "MVC", Author = "Nitish", 
        //            Description = "This is the description for MVC book",
        //            Category = "Programming", Language = "English", TotalPages = 134
        //        },
        //        new BookModel() { Id = 2, Title = "Dot Net Core", Author = "Nitish", 
        //            Description = "This is the description for Dot Net Core book",
        //            Category = "Framework", Language = "Chinese", TotalPages = 567
        //        },
        //        new BookModel() { Id = 3, Title = "C#", Author = "Monika",
        //            Description = "This is the description for C# book",
        //            Category = "Developer", Language = "Hindi", TotalPages = 897
        //        },
        //        new BookModel() { Id = 4, Title = "Java", Author = "Webgentle", 
        //            Description = "This is the description for Java book",
        //            Category = "Concept", Language = "English", TotalPages = 564
        //        },
        //        new BookModel() { Id = 5, Title = "Php", Author = "Webgentle",
        //            Description = "This is the description for Php book",
        //            Category = "Programming", Language = "English", TotalPages = 100
        //        },
        //        new BookModel() { Id = 6, Title = "Azure DevOps", Author = "NItish",
        //            Description = "This is the description for Azure DevOps book",
        //            Category = "DevOps", Language = "English", TotalPages = 800
        //        }
        //    };
        //}
    }
}
