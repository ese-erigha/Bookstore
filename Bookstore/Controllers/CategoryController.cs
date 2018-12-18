using AutoMapper;
using System.Web.Http;
using System.Threading.Tasks;
using Bookstore.Service.Interfaces;
using ResponseDto = Bookstore.Model.ResponseDto;
using CreateDto = Bookstore.Model.CreateDto;
using UpdateDto = Bookstore.Model.UpdateDto;
using Entity = Bookstore.Entities.Implementations;
using Bookstore.Filters;
using System.Net;

namespace Bookstore.Controllers
{
    [RoutePrefix("api/v1/category")]
    public class CategoryController : BaseApiController<Entity.Category, ResponseDto.Category>
    {
        readonly IMapper _mapper;
        readonly ICategoryService _service;

        public CategoryController(IMapper mapper, ICategoryService service): base(mapper, service)
        {
            _service = service;
            _mapper = mapper;
        }

        public CategoryController() :base()
        {

        }

        
        [Route("")]
        [HttpPost]
        [ValidateModel]
        public async Task<IHttpActionResult> CreateCategory([FromBody] CreateDto.Category viewModel)
        {
            return await Create(viewModel);
        }

        [ValidateModel]
        [Route("{id:long}")]
        [HttpPut]
        public async Task<IHttpActionResult> UpdateCategory(long id, [FromBody] UpdateDto.Category viewModel)
        {
            var entity = await _service.GetByIdAsync(id);
            if (entity == null)
            {
                return Content(HttpStatusCode.BadRequest, "Item does not exist");
            }
            _mapper.Map(viewModel, entity);
            return await Update(entity);
        }

    }
}
