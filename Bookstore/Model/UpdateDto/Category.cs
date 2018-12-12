using System;
using Bookstore.Model.BaseDto;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Web.Http;
using Bookstore.Service.Interfaces;

namespace Bookstore.Model.UpdateDto
{
    public class Category: BaseCategoryDto, IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var scope = GlobalConfiguration.Configuration.DependencyResolver.BeginScope();
            ICategoryService _categoryService = scope.GetService(typeof(ICategoryService)) as ICategoryService;

            var validationResults = new List<ValidationResult>();

            var categories = _categoryService.FindBy(x => x.Name == Name);
            if (categories.Count > 0)

            {
                validationResults.Add(new ValidationResult("Item cannot be updated to another existing item", new string[] { "Name" }));
            }

            return validationResults;

        }
    }
}
