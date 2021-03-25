using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.Threading;
using System.Threading.Tasks;
using TouristClubApi.Data.Models;
using TransactionManagementAPI.Data;
using TransactionManagementAPI.Data.Models;

namespace TransactionManagementAPI.Features.Commands.UsersCRUD
{
    public class LoginUser
    {
        public class Command : IRequest<bool>
        {
            public LoginModel Model { get; set; }

            public Command(LoginModel model)
            {
                Model = model;
            }
        }

        public class Handler : IRequestHandler<LoginUser.Command, bool>
        {
            private readonly AppDbContext _context;
            private readonly UserManager<User> _userManager;
            private readonly IConfiguration _configuration;

            public Handler(AppDbContext context, UserManager<User> userManager, IConfiguration configuration)
            {
                _context = context;
                _userManager = userManager;
                _configuration = configuration;
            }

            public async Task<bool> Handle(Command command, CancellationToken cancellationToken)
            {
                return false;
            }
        }
    }
}