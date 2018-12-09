using System;
using System.Collections.Generic;

namespace Bookstore.Entities.Implementations
{
    public class Author: BaseEntity
    {
        public string FullName { get; set; }

        public ICollection<Book> Books { get; set; }
    }
}
