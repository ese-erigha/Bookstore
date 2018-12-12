using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Bookstore.Model.BaseDto;
using Bookstore.Service.Interfaces;
using System.Web.Http;

namespace Bookstore.Model.CreateDto
{
    public class Category: BaseCategoryDto ,IValidatableObject
    {
        
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var scope = GlobalConfiguration.Configuration.DependencyResolver.BeginScope();
            ICategoryService _categoryService = scope.GetService(typeof(ICategoryService)) as ICategoryService;
            
            var validationResults = new List<ValidationResult>();

            var categories = _categoryService.FindBy(x => x.Name == Name);
            if (categories.Count > 0)
            {
                validationResults.Add(new ValidationResult("Category already exist", new string[] { "Name" }));
            }

            return validationResults;

        } 
    }
}
