using System;
using System.ComponentModel.DataAnnotations;

namespace Bookstore.Model.BaseDto
{
    public class BaseAuthorDto: BaseModel
    {
        [Required(ErrorMessage = "FullName is required")]
        public string FullName { get; set; }
    }
}
