using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Bookstore.Helpers;
using Bookstore.Service.Interfaces;

namespace Bookstore.Model.BaseDto
{
    public class BaseBookDto : BaseModel
    {
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Isbn is required")]
        public string Isbn { get; set; }

        [Required(ErrorMessage = "Page Count is required")]
        public int? PageCount { get; set; }

        [Required(ErrorMessage = "PublishedDate is required")]
        public DateTime PublishedDate { get; set; }


        [Required(ErrorMessage = "ThumbnailUrl is required")]
        public string ThumbnailUrl { get; set; }

        public string LongDescription { get; set; }

        [Required(ErrorMessage = "Status is required")]
        public string Status { get; set; }

        [Required(ErrorMessage = "Existing Category is required")]
        public IEnumerable<ExistingCategory> ExistingCategories { get; set; }


        public IEnumerable<NewCategory> NewCategories { get; set; }

        [Required(ErrorMessage = "Existing Author(s) is required")]
        public IEnumerable<ExistingAuthor> ExistingAuthors { get; set; }


        public IEnumerable<NewAuthor> NewAuthors { get; set; }


        public List<ValidationResult> ValidateAuthors(List<ValidationResult> validationResults, IAuthorService service)
        {
            validationResults = ValidateNewAuthors(validationResults, service);
            validationResults = ValidateExistingAuthors(validationResults, service);
            return validationResults;
        }

        public List<ValidationResult> ValidateNewAuthors(List<ValidationResult> validationResults, IAuthorService service)
        {
            foreach (var author in NewAuthors)
            {
                var authors = service.FindBy(x => x.FullName == author.FullName);
                if (authors.Count > 0)
                {
                    validationResults.Add(new ValidationResult(author.FullName + " already exist", new string[] { "NewAuthors" }));
                }
            }

            return validationResults;
        }

        public List<ValidationResult> ValidateExistingAuthors(List<ValidationResult> validationResults, IAuthorService service)
        {
            foreach (var author in ExistingAuthors)
            {
                var authors = service.FindBy(x => x.FullName == author.FullName);
                if (authors.Count < 1)
                {
                    validationResults.Add(new ValidationResult(author.FullName + " does not exist", new string[] { "ExistingAuthors" }));
                }
            }

            return validationResults;
        }

        public List<ValidationResult> ValidateStatus(List<ValidationResult> validationResults)
        {
            var statusList = new List<string> { "Published", "Unpublished" };
            if (!statusList.Contains(Status))
            {
                validationResults.Add(new ValidationResult("Please choose between Published and Unpublished state", new string[] { "Status" }));
            }

            return validationResults;
        }

        public List<ValidationResult> ValidateCategories(List<ValidationResult> validationResults, ICategoryService service)
        {
            validationResults = ValidateNewCategories(validationResults, service);
            validationResults = ValidateExistingCategories(validationResults, service);
            return validationResults;
        }

        public List<ValidationResult> ValidateNewCategories(List<ValidationResult> validationResults, ICategoryService service)
        {
            foreach (var category in NewCategories)
            {
                var categories = service.FindBy(x => x.Name == category.Name);
                if (categories.Count > 0)
                {
                    validationResults.Add(new ValidationResult(category.Name + " already exist", new string[] { "NewCategories" }));
                }
            }

            return validationResults;
        }

        public List<ValidationResult> ValidateExistingCategories(List<ValidationResult> validationResults, ICategoryService service)
        {
            foreach (var category in ExistingCategories)
            {
                var categories = service.FindBy(x => x.Id == category.Id);
                if (categories.Count < 1)
                {
                    validationResults.Add(new ValidationResult(category.Name + " with Id: " + category.Id + " does not exist", new string[] { "ExistingCategories" }));
                }
            }

            return validationResults;
        }


        public void AssertLongDescription()
        {
            if (LongDescription == string.Empty) LongDescription = null;
        }
    }
}
