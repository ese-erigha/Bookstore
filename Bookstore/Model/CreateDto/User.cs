using Bookstore.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Bookstore.Model.CreateDto
{
    public class User : IValidatableObject
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage="Please provide a valid email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }


        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
           
            var scope = GlobalConfiguration.Configuration.DependencyResolver.BeginScope();
            IAccountService _service = scope.GetService(typeof(IAccountService)) as IAccountService;
            List<ValidationResult> validationResults = new List<ValidationResult>();

            var users = _service.GetUsersByAsync(x => x.Email == Email).ToList();
            if (users.Count > 0)
            {
                validationResults.Add((new ValidationResult("User with email already exist", new string[] { "Email" })));
            }

            return validationResults;
        }
    }
}