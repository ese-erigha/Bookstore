using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Bookstore.Model.BaseDto;
using Bookstore.Service.Interfaces;
using System.Web.Http;
using System.Web.Http.Dependencies;

namespace Bookstore.Model.UpdateDto
{
    public class Book: BaseBookDto, IValidatableObject
    {
    
        private IDependencyScope scope;

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            scope = GlobalConfiguration.Configuration.DependencyResolver.BeginScope();

            var service = scope.GetService(typeof(IBookService)) as IBookService;

            var validationResults = new List<ValidationResult>();

            validationResults = ValidateTitle(validationResults, service);

            validationResults = ValidateStatus(validationResults);

            var catService = scope.GetService(typeof(ICategoryService)) as ICategoryService;

            validationResults = ValidateCategories(validationResults, catService);

            var authorService = scope.GetService(typeof(IAuthorService)) as IAuthorService;

            validationResults = ValidateAuthors(validationResults, authorService);

            AssertLongDescription();

            return validationResults;
        }

        public List<ValidationResult> ValidateTitle(List<ValidationResult> validationResults, IBookService service)
        {
            var books = service.FindBy(x => x.Title == Title);
            if (books.Count > 0)
            {
                validationResults.Add(new ValidationResult("Item cannot be updated to another existing item", new string[] { "FullName" }));
            }

            return validationResults;
        }
    }
}
