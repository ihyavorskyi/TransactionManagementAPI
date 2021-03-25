using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TouristClubApi.Data.Models;
using TransactionManagementAPI.Data.Models;
using TransactionManagementAPI.Features.Commands.UsersCRUD;

namespace TransactionManagementAPI.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync(RegisterModel model)
        {
            var command = new RegisterUser.Command(model);
            var res = await _mediator.Send(command);
            return Ok(res);
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync(LoginModel model)
        {
            var command = new LoginUser.Command(model);
            var res = await _mediator.Send(command);
            return Ok(res);
        }
    }
}