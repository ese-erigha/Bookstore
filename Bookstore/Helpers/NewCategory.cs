using System;
using System.ComponentModel.DataAnnotations;

namespace Bookstore.Helpers
{
    public class NewCategory
    {
        [Required(ErrorMessage = "Category Name is required")]
        public string Name { get; set; }
    }
}
