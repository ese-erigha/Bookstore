using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Bookstore.Model.BaseDto;
using Bookstore.Service.Interfaces;
using System.Web.Http;

namespace Bookstore.Model.CreateDto
{
    public class Book: BaseBookDto, IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var scope = GlobalConfiguration.Configuration.DependencyResolver.BeginScope();
            
            var service = scope.GetService(typeof(IBookService)) as IBookService;

            var validationResults = new List<ValidationResult>();

            validationResults = ValidateTitle(validationResults, service);

            validationResults = ValidateStatus(validationResults);

            var categoryService = scope.GetService(typeof(ICategoryService)) as ICategoryService; 

            validationResults = ValidateCategories(validationResults, categoryService);

            var authorService = scope.GetService(typeof(IAuthorService)) as IAuthorService;

            validationResults = ValidateAuthors(validationResults, authorService);

            AssertLongDescription();

            return validationResults;
        }

        List<ValidationResult> ValidateTitle(List<ValidationResult> validationResults, IBookService service)
        {
            var book = service.FindBy(x => x.Title == Title);
            if (book.Count > 0)
            {
                validationResults.Add(new ValidationResult("Book already exist", new string[] { "Book" }));
            }

            return validationResults;
        }

       
    }
}
