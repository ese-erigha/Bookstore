using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Bookstore.Model.BaseDto;
using Bookstore.Service.Interfaces;

namespace Bookstore.Model.CreateDto
{
    public class Author : BaseAuthorDto, IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var service = (IAuthorService)validationContext.GetService(typeof(IAuthorService));
            var validationResults = new List<ValidationResult>();

            var author = service.FindBy(x => x.FullName == FullName);
            if(author.Count > 0)
            {
                validationResults.Add(new ValidationResult("Author already exist", new string[] { "Author" }));
            }

            return validationResults;
        }
    }
}
