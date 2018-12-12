using ResponseDto = Bookstore.Model.ResponseDto;
using CreateDto = Bookstore.Model.CreateDto;
using UpdateDto = Bookstore.Model.UpdateDto;
using AutoMapper;
using Bookstore.Service.Interfaces;
using System.Web.Http;
using System.Threading.Tasks;
using Entity = Bookstore.Entities.Implementations;
using Bookstore.Helpers;
using System.Net;
using System.Collections.Generic;
using Bookstore.Filters;

namespace Bookstore.Controllers
{
    [RoutePrefix("api/v1/book")]
    public class BookController: BaseApiController<Entity.Book, ResponseDto.Book>
    {
        readonly IMapper _mapper;
        readonly IBookService _service;
        readonly ICategoryService _categoryService;
        readonly IAuthorService _authorService;

        public BookController(IMapper mapper, IBookService service, ICategoryService categoryService, IAuthorService authorService) : base(mapper, service)
        {
            _service = service;
            _mapper = mapper;
            _categoryService = categoryService;
            _authorService = authorService;
        }

        [ValidateModel]
        [Route("")]
        [HttpPost]
        public async Task<IHttpActionResult> CreateBook([FromBody] CreateDto.Book viewModel)
        {
            Entity.Book bookEntity = _mapper.Map<Entity.Book>(viewModel);

            foreach(NewCategory newCategory in viewModel.NewCategories)
            {
                var category = new Entity.Category { Name = newCategory.Name };
                bookEntity.Categories.Add(category);
            }

            foreach (ExistingCategory existingCategory in viewModel.ExistingCategories)
            {
                Entity.Category category = await _categoryService.GetByIdAsync(existingCategory.Id);
                bookEntity.Categories.Add(category);
            }

            foreach (NewAuthor newAuthor in viewModel.NewAuthors)
            {
                var author = new Entity.Author { FullName = newAuthor.FullName };
                bookEntity.Authors.Add(author);
            }

            foreach (ExistingAuthor existingAuthor in viewModel.ExistingAuthors)
            {
                Entity.Author author = await _authorService.GetByIdAsync(existingAuthor.Id);
                bookEntity.Authors.Add(author);
            }

            var state = await _service.Commit();

            if (!state)
            {
                return Content(HttpStatusCode.InternalServerError, "A problem occurred while handling your request");
            }

            var modelToReturn = _mapper.Map<ResponseDto.Book>(bookEntity);

            return CreatedAtRoute(
                routeName: "GetSingleModel",
                routeValues: new { id = modelToReturn.Id },
                content: modelToReturn
            );

        }

        [ValidateModel]
        [Route("{id:long}")]
        [HttpPut]
        public async Task<IHttpActionResult> UpdateBook(long id, [FromBody] UpdateDto.Book viewModel)
        {
            Entity.Book bookEntity = await _service.GetByIdAsync(id);

            if(bookEntity == null)
            {
                return Content(HttpStatusCode.NotFound, "Item does not exist");
            }

            _mapper.Map(viewModel,bookEntity); //Updates Entity with ViewModel attributes

            ICollection<Entity.Category> categories = new List<Entity.Category>();
            ICollection<Entity.Author> authors = new List<Entity.Author>();

            foreach (NewCategory newCategory in viewModel.NewCategories)
            {
                var category = new Entity.Category { Name = newCategory.Name };
                categories.Add(category);
            }

            foreach (ExistingCategory existingCategory in viewModel.ExistingCategories)
            {
                Entity.Category category = await _categoryService.GetByIdAsync(existingCategory.Id);
                categories.Add(category);
            }

            bookEntity.Categories = categories;

            foreach (NewAuthor newAuthor in viewModel.NewAuthors)
            {
                var author = new Entity.Author { FullName = newAuthor.FullName };
                authors.Add(author);
            }

            foreach (ExistingAuthor existingAuthor in viewModel.ExistingAuthors)
            {
                Entity.Author author = await _authorService.GetByIdAsync(existingAuthor.Id);
                authors.Add(author);
            }

            bookEntity.Authors = authors;

            var state = await _service.Commit();

            if (!state)
            {
                return Content(HttpStatusCode.InternalServerError, "A problem occurred while handling your request");
            }

            return Content(HttpStatusCode.NoContent, "Update was successful");
        }
    }
}
