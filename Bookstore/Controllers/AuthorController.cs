using ResponseDto = Bookstore.Model.ResponseDto;
using CreateDto = Bookstore.Model.CreateDto;
using UpdateDto = Bookstore.Model.UpdateDto;
using AutoMapper;
using Bookstore.Service.Interfaces;
using System.Web.Http;
using System.Threading.Tasks;
using Entity = Bookstore.Entities.Implementations;
using Bookstore.Filters;
using System.Net;
using Microsoft.Web.Http;

namespace Bookstore.Controllers
{
    [ApiVersion("1.0")]
    [RoutePrefix("api/v1/author")]
    public class AuthorController : BaseApiController<Entity.Author, ResponseDto.Author>
    {
        readonly IMapper _mapper;
        readonly IAuthorService _service;


        public AuthorController(IMapper mapper, IAuthorService service) : base(mapper, service)
        {
            _service = service;
            _mapper = mapper;
        }

        [ValidateModel]
        [Route("")]
        [HttpPost]
        public async Task<IHttpActionResult> CreateAuthor([FromBody] CreateDto.Author viewModel)
        {
            return await Create(viewModel);
        }

        [ValidateModel]
        [Route("{id:long}")]
        [HttpPut]
        public async Task<IHttpActionResult> UpdateAuthor(long id, [FromBody] UpdateDto.Author viewModel)
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
