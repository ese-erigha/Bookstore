using Bookstore.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using CreateDto = Bookstore.Model.CreateDto;
using Entity = Bookstore.Entities.Implementations;
using AutoMapper;
using System.Threading.Tasks;
using Bookstore.Filters;

namespace Bookstore.Controllers
{
    [RoutePrefix("api/v1/auth")]
    public class AccountController : ApiController
    {
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;

        public AccountController(IMapper mapper,IAccountService accountService)
        {
            _accountService = accountService;
            _mapper = mapper;
        }


        [ValidateModel]
        [Route("register")]
        [HttpPost]
        public async Task<IHttpActionResult> Register(CreateDto.User userModel)
        {
            var user = _mapper.Map<Entity.ApplicationUser>(userModel);
            IdentityResult result = await _accountService.CreateAsync(user, userModel.Password);
            if (result.Succeeded)
            {
                result = await _accountService.AddUserToRoleAsync(user, "Admin");
                if (result.Succeeded)
                {
                    return Content(HttpStatusCode.Created, "Account created successfully");
                }
            }
            return StatusCode(HttpStatusCode.InternalServerError);
        }


    }
}