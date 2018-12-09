using System;
using System.ComponentModel.DataAnnotations;

namespace Bookstore.Model.BaseDto
{
    public class BaseCategoryDto : BaseModel
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
    }
}
