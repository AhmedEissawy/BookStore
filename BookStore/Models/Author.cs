using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public class Author
    {

        public Author()
        {
            Books = new HashSet<Book>();
        }

        public int Id { get; set; }

        [Required]
        public string FullName { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}
