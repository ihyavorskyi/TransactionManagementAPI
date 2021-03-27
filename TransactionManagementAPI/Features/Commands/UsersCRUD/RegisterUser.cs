using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading;
using System.Threading.Tasks;
using TouristClubApi.Data.Models;
using TransactionManagementAPI.Data.Models;

namespace TransactionManagementAPI.Features.Commands.UsersCRUD
{
    /// <summary>
    /// Class for registring new user
    /// </summary>
    public class RegisterUser
    {
        public class Command : IRequest<string>
        {
            /// <summary>
            /// New user register model
            /// </summary>
            public RegisterModel Model { get; set; }

            public Command(RegisterModel model)
            {
                Model = model;
            }
        }

        public class Handler : IRequestHandler<RegisterUser.Command, string>
        {
            private readonly UserManager<User> _userManager;

            public Handler(UserManager<User> userManager)
            {
                _userManager = userManager;
            }

            public async Task<string> Handle(Command command, CancellationToken cancellationToken)
            {
                // Checking user exist
                var userExist = await _userManager.FindByNameAsync(command.Model.UserName);
                if (userExist != null)
                {
                    return "User already exist";
                }

                // Creating new user
                var user = new User()
                {
                    Email = command.Model.Email,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = command.Model.UserName
                };

                // Adding new user to DataBase and result checking
                var result = await _userManager.CreateAsync(user, command.Model.Password);
                if (!result.Succeeded)
                {
                    return "Failed to create user";
                }

                return "User created successfully";
            }
        }
    }
}