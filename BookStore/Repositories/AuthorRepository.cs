using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Repositories
{
    public class AuthorRepository : IBookStoreRepository<Author>
    {
       private readonly ApplicationDbContext _context;

        public AuthorRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(Author author)
        {
            _context.Authors.Add(author);

            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var author = Find(id);

            _context.Authors.Remove(author);

            _context.SaveChanges();
        }

        public Author Find(int id)
        {
            var author = _context.Authors.SingleOrDefault(a => a.Id == id);

            return author;
        }

        public List<Author> GetAll()
        {
            return _context.Authors.ToList();
        }

        public IList<Author> Search(string term)
        {
            var result = _context.Authors.Where(a => a.FullName.Contains(term)).ToList();

            return result;
        }

        public void Update(int id, Author author)
        {
            var existAuthor = Find(id);

            existAuthor.FullName = author.FullName;

            _context.Authors.Update(existAuthor);

            _context.SaveChanges();
        }
    }
}
