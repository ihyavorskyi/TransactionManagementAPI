using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TouristClubApi.Data;
using TouristClubApi.Data.Models;
using TransactionManagementAPI.Data.Models;

namespace TransactionManagementAPI.Features.Commands.UsersCRUD
{
    public class RegisterUser
    {
        public class Command : IRequest<bool>
        {
            public RegisterModel Model { get; set; }

            public Command(RegisterModel model)
            {
                Model = model;
            }
        }

        public class Handler : IRequestHandler<RegisterUser.Command, bool>
        {
            private readonly AppDbContext _context;
            private readonly UserManager<User> _userManager;

            public Handler(AppDbContext context, UserManager<User> userManager)
            {
                _context = context;
                _userManager = userManager;
            }

            public async Task<bool> Handle(Command command, CancellationToken cancellationToken)
            {
                var userExist = await _userManager.FindByNameAsync(command.Model.UserName);
                if (userExist != null)
                {
                    return false;
                }

                var user = new User()
                {
                    Email = command.Model.Email,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = command.Model.UserName
                };

                var result = await _userManager.CreateAsync(user, command.Model.Password);
                if (!result.Succeeded)
                {
                    return false;
                }

                return true;
            }
        }
    }
}