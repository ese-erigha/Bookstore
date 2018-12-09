using System;
using System.Collections.Generic;

namespace Bookstore.Entities.Implementations
{
    public class Book : BaseEntity
    {
        public string Title { get; set; }

        public string Isbn { get; set; }

        public int PageCount { get; set; }

        public string ThumbnailUrl { get; set; }

        public DateTime PublishedDate { get; set; }

        public string LongDescription { get; set; }

        public string Status { get; set; }

        public virtual ICollection<Author> Authors { get; set; }

        public virtual ICollection<Category> Categories { get; set; }
    }
}
