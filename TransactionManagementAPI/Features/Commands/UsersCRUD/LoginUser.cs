using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TouristClubApi.Data.Models;
using TransactionManagementAPI.Data.DTOs;
using TransactionManagementAPI.Data.Models;

namespace TransactionManagementAPI.Features.Commands.UsersCRUD
{
    /// <summary>
    /// Class for user login and creating new JWT for them
    /// </summary>
    public class LoginUser
    {
        public class Command : IRequest<LoginResponse>
        {
            /// <summary>
            /// User login model
            /// </summary>
            public LoginModel Model { get; set; }

            public Command(LoginModel model)
            {
                Model = model;
            }
        }

        public class Handler : IRequestHandler<LoginUser.Command, LoginResponse>
        {
            private readonly UserManager<User> _userManager;

            public Handler(UserManager<User> userManager)
            {
                _userManager = userManager;
            }

            public async Task<LoginResponse> Handle(Command command, CancellationToken cancellationToken)
            {
                // If there is no model return
                if (command.Model == null)
                {
                    return new LoginResponse() { Message = "Invalid client request", Token = "" };
                }

                // Find user in DataBase
                var user = await _userManager.FindByNameAsync(command.Model.UserName);

                // If user exist creating new JWT for him and authorizes in the system
                if (user != null && await _userManager.CheckPasswordAsync(user, command.Model.Password))
                {
                    var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"));
                    var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                    var tokeOptions = new JwtSecurityToken(
                        issuer: "http://localhost:5000",
                        audience: "http://localhost:5000",
                        claims: new List<Claim>(),
                        expires: DateTime.Now.AddMinutes(15),
                        signingCredentials: signinCredentials
                    );
                    var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
                    return new LoginResponse() { Message = "Token received", Token = tokenString };
                }
                else
                {
                    return new LoginResponse() { Message = "User not found", Token = "" };
                }
            }
        }
    }
}