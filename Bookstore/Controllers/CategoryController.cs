using AutoMapper;
using System.Web.Http;
using System.Threading.Tasks;
using Bookstore.Service.Interfaces;
using ResponseDto = Bookstore.Model.ResponseDto;
using CreateDto = Bookstore.Model.CreateDto;
using Entity = Bookstore.Entities.Implementations;

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

        [Route("")]
        [HttpPost]
        public async Task<IHttpActionResult> CreateCategory([FromBody] CreateDto.Category viewModel)
        {
            return await Create(viewModel);
        }

        [Route("{id:long}")]
        [HttpPut]
        public async Task<IHttpActionResult> UpdateCategory(long id, [FromBody] CreateDto.Category viewModel)
        {
            return await Update(id, viewModel);
        }

    }
}
