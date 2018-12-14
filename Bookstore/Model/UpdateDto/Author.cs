using System;
using Bookstore.Model.BaseDto;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Web.Http;
using Bookstore.Service.Interfaces;

namespace Bookstore.Model.UpdateDto
{
    public class Author : BaseAuthorDto, IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var scope = GlobalConfiguration.Configuration.DependencyResolver.BeginScope();
            IAuthorService _authorService = scope.GetService(typeof(IAuthorService)) as IAuthorService;

            var validationResults = new List<ValidationResult>();

            var authors = _authorService.FindBy(x => x.FullName == FullName);
            if (authors.Count > 0)
            {
                validationResults.Add(new ValidationResult("Item cannot be updated to another existing item", new string[] { "FullName" }));
            }

            return validationResults;
        }
    }
}
