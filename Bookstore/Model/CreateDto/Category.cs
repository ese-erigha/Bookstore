using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Bookstore.Model.BaseDto;
using Bookstore.Service.Interfaces;

namespace Bookstore.Model.CreateDto
{
    public class Category: BaseCategoryDto, IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var service = (ICategoryService)validationContext.GetService(typeof(ICategoryService));
            var validationResults = new List<ValidationResult>();

            var author = service.FindBy(x => x.Name == Name);
            if (author.Count > 0)
            {
                validationResults.Add(new ValidationResult("Category already exist", new string[] { "Name" }));
            }

            return validationResults;

        }
    }
}
