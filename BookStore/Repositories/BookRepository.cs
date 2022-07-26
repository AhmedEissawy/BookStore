using BookStore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Repositories
{
    public class BookRepository : IBookStoreRepository<Book>
    {
        private readonly ApplicationDbContext _context;

        public BookRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(Book book)
        {
            _context.Books.Add(book);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var book = Find(id);
            _context.Remove(book);
            _context.SaveChanges();
        }

        public Book Find(int id)
        {
           var book = _context.Books.Include(b=>b.Author).SingleOrDefault(b => b.Id == id);

            return book;
        }

        public List<Book> GetAll()
        {
            return _context.Books.Include(b=>b.Author).ToList();
        }

        public void Update(int id, Book book)
        {
            var existBook = Find(id);

            existBook.Title = book.Title;

            existBook.Description = book.Description;

            existBook.Author = book.Author;

            existBook.Image = book.Image;

            _context.Books.Update(existBook);

            _context.SaveChanges();

        }

        public IList<Book> Search(string term)
        {
            var result = _context.Books.Include(b => b.Author).Where(
                b => b.Title.Contains(term) ||
                b.Description.Contains(term) ||
                b.Author.FullName.Contains(term)).ToList();

            return result;
        }
    }
}
