using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TouristClubApi.Data.Models;
using TransactionManagementAPI.Data.Models;
using TransactionManagementAPI.Features.Commands.UsersCRUD;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace TransactionManagementAPI.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IMediator _mediator;
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;

        public UserController(IMediator mediator, UserManager<User> userManager, IConfiguration configuration)
        {
            _mediator = mediator;
            _userManager = userManager;
            _configuration = configuration;
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
            //var command = new LoginUser.Command(model);
            //var res = await _mediator.Send(command);
            //return Ok(res);

            //var user = await _userManager.FindByNameAsync(model.UserName);
            //if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            //{
            //    var authClaims = new List<Claim>
            //        {
            //            new Claim(ClaimTypes.Name,user.UserName),
            //            new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
            //        };

            //    var authSingingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            //    var token = new JwtSecurityToken(
            //        issuer: _configuration["JWT:ValidIssuer"],
            //        audience: _configuration["JWT:ValidAudience"],
            //        expires: DateTime.Now.AddHours(3),
            //        claims: authClaims,
            //        signingCredentials: new SigningCredentials(authSingingKey, SecurityAlgorithms.HmacSha256)
            //    );
            //    return Ok(new
            //    {
            //        token = new JwtSecurityTokenHandler().WriteToken(token),
            //        expiration = token.ValidTo,
            //        User = user.UserName
            //    });
            //}
            //return Unauthorized();
        }
    }
}