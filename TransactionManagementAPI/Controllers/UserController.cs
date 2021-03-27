using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TransactionManagementAPI.Data.Models;
using TransactionManagementAPI.Features.Commands.UsersCRUD;

namespace TransactionManagementAPI.Controllers
{
    /// <summary>
    /// Used to manage users in the system.
    /// Сan be used by unauthorized users.
    /// </summary>
    [Route("api/users")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Post method for registration new user.
        /// </summary>
        /// <param name="model"> User registration model </param>
        /// <returns> Operation status </returns>
        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync(RegisterModel model)
        {
            var command = new RegisterUser.Command(model);
            var res = await _mediator.Send(command);
            return Ok(res);
        }

        /// <summary>
        /// Post method for login user.
        /// </summary>
        /// <param name="model"> User login model </param>
        /// <returns> Operation status and JWT for using the system </returns>
        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync(LoginModel model)
        {
            var command = new LoginUser.Command(model);
            var res = await _mediator.Send(command);
            return Ok(res);
        }
    }
}