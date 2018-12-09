using System;
using System.Collections.Generic;

namespace Bookstore.Entities.Implementations
{
    public class Category: BaseEntity
    {
        public string Name { get; set; }

        public ICollection<Book> Books { get; set; }
    }
}
