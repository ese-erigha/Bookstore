using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Bookstore.Model.BaseDto;
using Bookstore.Service.Interfaces;

namespace Bookstore.Model.CreateDto
{
    public class Book: BaseBookDto, IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var service = (IBookService)validationContext.GetService(typeof(IBookService));

            var validationResults = new List<ValidationResult>();

            validationResults = ValidateTitle(validationResults, service);

            validationResults = ValidateStatus(validationResults);

            var catService = (ICategoryService)validationContext.GetService(typeof(ICategoryService));

            validationResults = ValidateCategories(validationResults, catService);

            var authorService = (IAuthorService)validationContext.GetService(typeof(IAuthorService));

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
