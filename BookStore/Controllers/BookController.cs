using BookStore.Models;
using BookStore.Repositories;
using BookStore.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Controllers
{
    public class BookController : Controller
    {

        private readonly IBookStoreRepository<Book> _storeRepository;

        private readonly IBookStoreRepository<Author> _authorRepository;

        private readonly IHostingEnvironment _hosting;

        public BookController(IBookStoreRepository<Book> storeRepository,IBookStoreRepository<Author> authorRepository,IHostingEnvironment hosting)
        {
            _storeRepository = storeRepository;
            _authorRepository = authorRepository;
            _hosting = hosting;

        }

        // GET: BookController
        public ActionResult Index()
        {
            var books = _storeRepository.GetAll();

            return View(books);
        }

        // GET: BookController/Details/5
        public ActionResult Details(int id)
        {
            var book = _storeRepository.Find(id);

            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // GET: BookController/Create
        public ActionResult Create()
        {
            var model = new BookViewModel
            {
                Authors = _authorRepository.GetAll()
            };

            return View(model);
        }

        // POST: BookController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BookViewModel bookViewModel)
        {

            if (ModelState.IsValid)
            {
                Upload(bookViewModel);

                var author = _authorRepository.Find(bookViewModel.AuthorId);

                Book book = new Book
                {
                    Title = bookViewModel.Title,
                    Description = bookViewModel.Description,
                    Author = author,
                    Image = bookViewModel.Image
                };

                _storeRepository.Add(book);

                return RedirectToAction(nameof(Index));
            }
            else
            {
                bookViewModel.Authors = _authorRepository.GetAll();

                ModelState.AddModelError("", "You should fill all fields");

                return View(bookViewModel);
            }

           
        }
            
        // GET: BookController/Edit/5
        public ActionResult Edit(int id)
        {
            var book = _storeRepository.Find(id);

            var viewModel = new BookViewModel
            {
                Title = book.Title,
                Description = book.Description,
                BookId = book.Id,
                Image = book.Image,
                AuthorId = book.Author.Id,
                Authors = _authorRepository.GetAll()
            };

            return View(viewModel);
        }

        // POST: BookController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id,BookViewModel viewModel)
        {
            var existedBook = _storeRepository.Find(id);

            if (ModelState.IsValid)
            {

                Upload(viewModel);

                existedBook.Title = viewModel.Title;

                existedBook.Description = viewModel.Description;

                existedBook.AuthorId = viewModel.AuthorId;

                existedBook.Image = viewModel.Image;

                _storeRepository.Update(id, existedBook);

                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(viewModel);
            }
        }

        // GET: BookController/Delete/5
        public ActionResult Delete(int id)
        {
            var book = _storeRepository.Find(id);

            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: BookController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmDelete(int id)
        {

            _storeRepository.Delete(id);

            return RedirectToAction("Index");
        }

        public ActionResult Search(string term)
        {
           var result = _storeRepository.Search(term);

            return View("Index", result);
        }

        void Upload(BookViewModel bookViewModel)
        {
            if (bookViewModel.File != null)
            {
                string imagesFolder = Path.Combine(_hosting.WebRootPath,"Images/Books");

                string uniqueFileName = Guid.NewGuid() + ".jpg";

                string filePath = Path.Combine(imagesFolder, uniqueFileName);

                using (var FileStream = new FileStream(filePath, FileMode.Create))
                {
                    bookViewModel.File.CopyTo(FileStream);
                }

                bookViewModel.Image = uniqueFileName;
            }
        }
    }
}
