using System;
using System.ComponentModel.DataAnnotations;

namespace Bookstore.Helpers
{
    public class NewAuthor 
    {
        [Required(ErrorMessage = "Author Name is required")]
        public string FullName { get; set; }
    }
}
