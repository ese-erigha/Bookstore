using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using Bookstore.Model.BaseDto;
using Bookstore.Service.Interfaces;
using System.Web.Http;
using Autofac.Integration.WebApi;

namespace Bookstore.Model.CreateDto
{
    public class Author : BaseAuthorDto, IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var scope = GlobalConfiguration.Configuration.DependencyResolver.BeginScope();
            IAuthorService service = scope.GetService(typeof(IAuthorService)) as IAuthorService;
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
