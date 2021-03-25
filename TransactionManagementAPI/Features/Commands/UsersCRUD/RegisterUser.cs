using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading;
using System.Threading.Tasks;
using TouristClubApi.Data.Models;
using TransactionManagementAPI.Data;
using TransactionManagementAPI.Data.DTOs;
using TransactionManagementAPI.Data.Models;

namespace TransactionManagementAPI.Features.Commands.UsersCRUD
{
    public class RegisterUser
    {
        public class Command : IRequest<Response>
        {
            public RegisterModel Model { get; set; }

            public Command(RegisterModel model)
            {
                Model = model;
            }
        }

        public class Handler : IRequestHandler<RegisterUser.Command, Response>
        {
            private readonly UserManager<User> _userManager;

            public Handler(UserManager<User> userManager)
            {
                _userManager = userManager;
            }

            public async Task<Response> Handle(Command command, CancellationToken cancellationToken)
            {
                var userExist = await _userManager.FindByNameAsync(command.Model.UserName);
                if (userExist != null)
                {
                    return new Response() { Status = "Error", Message = "User already exist" };
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
                    return new Response() { Status = "Error", Message = "Failed to create user" };
                }

                return new Response() { Status = "Successfully", Message = "User created successfully" };
            }
        }
    }
}