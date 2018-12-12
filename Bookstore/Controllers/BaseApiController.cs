using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using Bookstore.Helpers;
using Bookstore.Service.Interfaces;
using ResponseDto = Bookstore.Model.ResponseDto;
using Bookstore.Model.BaseDto;
using Entity = Bookstore.Entities.Implementations;

namespace Bookstore.Controllers
{

    public class BaseApiController<E,R> : ApiController where E: Entity.BaseEntity where R : ResponseDto.Base
    {

        readonly IMapper _mapper;
        readonly IEntityService<E> _service;

        public BaseApiController(IMapper mapper, IEntityService<E> service)
        {
            _service = service;
            _mapper = mapper;
        }

        [Route("")]
        [HttpGet]
        public IHttpActionResult GetAll([FromUri]PaginationInfo paginationInfo)
        {
            PagedList<R> result = Paginate(_service.Paginate(paginationInfo), paginationInfo);
            return Ok(result);
            //return Content(HttpStatusCode.OK, "Hello From Base Controller");
        }

        public async Task<IHttpActionResult> Create<V>(V viewModel) where V : BaseModel
        {
            var entity = _mapper.Map<E>(viewModel);
            _service.Create(entity);
            var state = await _service.Commit();

            if (!state)
            {
                return Content(HttpStatusCode.InternalServerError, "A problem occurred while handling your request");
            }

            var modelToReturn = _mapper.Map<R>(entity);

            return Content(HttpStatusCode.Created,modelToReturn);
        }


        //Get Record
        [Route("{id:long}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetSingle(long id)
        {
            var entity = await _service.GetByIdAsync(id);
            if (entity == null)
            {
                return NotFound();
            }

            var model = _mapper.Map<R>(entity);
            return Ok(model);
        }

        //Update a Record
        public async Task<IHttpActionResult> Update(E entity)
        {
          
            _service.Update(entity);
            var state = await _service.Commit();

            if (!state)
            {
                return Content(HttpStatusCode.InternalServerError, "A problem occurred while handling your request");
            }
            else
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
        }

        //Delete a record
        [Route("{id:long}")]
        [HttpDelete]
        public async Task<IHttpActionResult> Delete(long id)
        {
            var entity = await _service.GetByIdAsync(id);

            if (entity == null)
            {
                return NotFound();
            }

            _service.Delete(entity);
            var state = await _service.Commit();
            return (!state) ? Content(HttpStatusCode.InternalServerError, "A problem occurred while handling your request ") : Content(HttpStatusCode.Accepted, "");
        }

        private PagedList<R> Paginate(PaginationQuery<E> paginationQuery, PaginationInfo paginationInfo)
        {
            var count = paginationQuery.Count;
            IEnumerable<R> mappedItems = _mapper.Map<IEnumerable<R>>(paginationQuery.Items);
            return new PagedList<R>(mappedItems, count, paginationInfo.PageNumber, paginationInfo.PageSize);
        }
    }
}
