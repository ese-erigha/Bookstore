using System;
using System.ComponentModel.DataAnnotations;

namespace Bookstore.Helpers
{
    public class ExistingCategory : NewCategory
    {
        [Required(ErrorMessage = "Category Id is required")]
        public long Id { get; set; }
    }
}
