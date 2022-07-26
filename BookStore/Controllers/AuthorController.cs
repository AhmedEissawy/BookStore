using BookStore.Models;
using BookStore.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Controllers
{
    public class AuthorController : Controller
    {

        private readonly IBookStoreRepository<Author> _storeRepository;

        public AuthorController(IBookStoreRepository<Author> storeRepository)
        {
            _storeRepository = storeRepository;
        }

        // GET: AuthorController
        public ActionResult Index()
        {
            var authors = _storeRepository.GetAll();

            return View(authors);
        }

        // GET: AuthorController/Details/5
        public ActionResult Details(int id)
        {
            var author = _storeRepository.Find(id);

            if (author == null)
            {
                return NotFound();
            }

            return View(author);
        }

        // GET: AuthorController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AuthorController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Author author)
        {
            if (ModelState.IsValid)
            {
                _storeRepository.Add(author);

                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
            
        }

        // GET: AuthorController/Edit/5
        public ActionResult Edit(int id)
        {
            var author = _storeRepository.Find(id);

            if (author == null)
            {
                return NotFound();
            }

            return View(author);
        }

        // POST: AuthorController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Author author)
        {
            if (ModelState.IsValid)
            {
                _storeRepository.Update(id,author);

                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
           
        }

        // GET: AuthorController/Delete/5
        public ActionResult Delete(int id)
        {
            var author = _storeRepository.Find(id);

            if (author == null)
            {
                return NotFound();
            }

            return View(author);
        }

        // POST: AuthorController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Author author)
        {
            try
            {
                _storeRepository.Delete(id);

                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return View();
            }
               
        }
    }
}
