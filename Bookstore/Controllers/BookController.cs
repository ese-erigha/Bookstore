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
using System.Linq;
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

        public BookController() : base()
        {

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

            _service.Create(bookEntity);

            var state = await _service.Commit();

            if (!state)
            {
                return Content(HttpStatusCode.InternalServerError, "A problem occurred while handling your request");
            }

            var modelToReturn = _mapper.Map<ResponseDto.Book>(bookEntity);

            return Content(HttpStatusCode.Created, modelToReturn);

        }

        [ValidateModel]
        [Route("{id:long}")]
        [HttpPut]
        public async Task<IHttpActionResult> UpdateBook(long id, [FromBody] UpdateDto.Book viewModel)
        {
            var bookEntity = await _service.GetByIdAsync(id);

            if(bookEntity == null)
            {
                return Content(HttpStatusCode.NotFound, "Item does not exist");
            }

            _mapper.Map(viewModel,bookEntity); //Updates Entity with ViewModel attributes

            //Delete every author in book entity that does not exist in existingAuthors Array
            bookEntity.Authors.ToList().RemoveAll(author => !viewModel.ExistingAuthors.Select(a => a.Id).Contains(author.Id));

            //Delete every category in book entity that does not exist in existingCategories Array
            bookEntity.Categories.ToList().RemoveAll(category =>!viewModel.ExistingCategories.Select(c => c.Id).Contains(category.Id));

            //Loop through newAuthors array and return items that do not exist in BaseEntity Authors array
            var newAuthors = viewModel.NewAuthors.Where(author => !bookEntity.Authors.Select(ba => ba.FullName).Contains(author.FullName)).ToList();
            
            //Same as above
            var newCategories = viewModel.NewCategories.Where(category => !bookEntity.Categories.Select(bc => bc.Name).Contains(category.Name)).ToList();

            newCategories.ForEach(category => bookEntity.Categories.Add(new Entity.Category { Name = category.Name }));

            newAuthors.ForEach(author => bookEntity.Authors.Add(new Entity.Author { FullName = author.FullName }));

            _service.Update(bookEntity);

            var state = await _service.Commit();

            if (!state)
            {
                return Content(HttpStatusCode.InternalServerError, "A problem occurred while handling your request");
            }

            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
