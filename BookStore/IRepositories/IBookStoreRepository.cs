using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Repositories
{
    public interface IBookStoreRepository<Entity>
    {
        List<Entity> GetAll();

        Entity Find(int id);

        void Add(Entity entity);

        void Update(int id , Entity entity);

        void Delete(int id);

        IList<Entity> Search(string term);
    }
}
