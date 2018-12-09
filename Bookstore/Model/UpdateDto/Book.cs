using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Bookstore.Model.BaseDto;
using Bookstore.Service.Interfaces;

namespace Bookstore.Model.UpdateDto
{
    public class Book: BaseBookDto, IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var service = (IBookService)validationContext.GetService(typeof(IBookService));

            var validationResults = new List<ValidationResult>();

            validationResults = ValidateStatus(validationResults);

            var catService = (ICategoryService)validationContext.GetService(typeof(ICategoryService));

            validationResults = ValidateCategories(validationResults, catService);

            var authorService = (IAuthorService)validationContext.GetService(typeof(IAuthorService));

            validationResults = ValidateAuthors(validationResults, authorService);

            AssertLongDescription();

            return validationResults;
        }
    }
}
