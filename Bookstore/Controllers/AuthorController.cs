using ResponseDto = Bookstore.Model.ResponseDto;
using CreateDto = Bookstore.Model.CreateDto;
using AutoMapper;
using Bookstore.Service.Interfaces;
using System.Web.Http;
using System.Threading.Tasks;
using Entity = Bookstore.Entities.Implementations;

namespace Bookstore.Controllers
{
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

        [Route("")]
        [HttpPost]
        public async Task<IHttpActionResult> CreateAuthor([FromBody] CreateDto.Author viewModel)
        {
            return await Create(viewModel);
        }

        [Route("{id:long}")]
        [HttpPut]
        public async Task<IHttpActionResult> UpdateAuthor(long id, [FromBody] CreateDto.Author viewModel)
        {
            return await Update(id, viewModel);
        }
    }
}
