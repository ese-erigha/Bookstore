using System;
namespace Bookstore.Model.ResponseDto
{
    public class Book : Base
    {
        public string Title { get; set; }

        public string Isbn { get; set; }

        public int PageCount { get; set; }

        public DateTime PublishedDate { get; set; }

        public string ThumbnailUrl { get; set; }

        public string LongDescription { get; set; }

        public string Status { get; set; }
    }
}
