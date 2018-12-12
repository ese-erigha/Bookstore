using System;
using System.ComponentModel.DataAnnotations;

namespace Bookstore.Helpers
{
    public class ExistingAuthor : NewAuthor
    {
        [Required(ErrorMessage = "Author Id is required")]
        public long Id { get; set; }
    }
}
