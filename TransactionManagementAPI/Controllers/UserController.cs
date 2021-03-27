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
        /// Login user into the system
        /// </summary>
        /// <remarks> Get JWT for authorization </remarks>
        /// <param name="userName"> The user name for login </param>
        /// <param name="password"> The password for login </param>
        /// <returns> Execution status and JWT for using the system </returns>
        [HttpGet("login")]
        public async Task<IActionResult> LoginAsync(string userName, string password)
        {
            var command = new LoginUser.Command(new LoginModel() { UserName = userName, Password = password });
            var res = await _mediator.Send(command);
            return Ok(res);
        }

        /// <summary> Create new user </summary>
        /// <param name="model"> User registration model </param>
        /// <returns> Execution status </returns>
        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync(RegisterModel model)
        {
            var command = new RegisterUser.Command(model);
            var res = await _mediator.Send(command);
            return Ok(res);
        }
    }
}