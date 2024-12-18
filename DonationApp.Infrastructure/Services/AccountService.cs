﻿using DonationApp.Core.Configurations;
using DonationApp.Core.Entities;
using DonationApp.Core.Interfaces;
using DonationApp.Core.Interfaces.Repositories;
using DonationApp.Core.Shared;
using DonationApp.UseCase.Models;
using DonationApp.UseCase.UseCases;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace DonationApp.Infrastructure.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly JwtOptions _jwtOptions;
        private readonly IUserAccountRepository _userAccountRepository;

        public AccountService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager,
            SignInManager<ApplicationUser> signInManager, IOptionsMonitor<JwtOptions> options, IUserAccountRepository userAccountRepository)
        {
            _userManager=userManager;
            _roleManager=roleManager;
            _signInManager=signInManager;
            _jwtOptions=options.CurrentValue;
            _userAccountRepository=userAccountRepository;
        }

        public async Task<Result<TokenModel>> LoginAsync(IModel model)
        {
            var loginModel = model as LoginModel;
            var user = await Validate(loginModel!);

            if (user is null)
            {
                return Result<TokenModel>.Failure("Invalid username or password");
            }
            else
            {
                var token = GenerateJwtToken(user);

                // phải lấy được account tương ứng thì mới đăng nhập được
                var account = await _userAccountRepository.FindByUserIdAsync(user.Id);

                var roles = await _userManager.GetRolesAsync(user);

                var rolestring = string.Join(",", roles);


                if (account is null)
                {
                    return Result<TokenModel>.Failure("Account not found");
                }

                return Result<TokenModel>.Success(new TokenModel
                {
                    UserId = user.Id,
                    AccessToken = token.Item1,
                    RefreshToken = token.Item2,
                    UserName = user.UserName!,
                    AccountNumber = account.AccountNumber,
                    FullName = user.FullName,
                    Roles = rolestring,
                    Balance = account.Balance
                });
            }
        }


        public async Task<bool> RegisterAsync(IModel model)
        {
            var registerModel = model as RegisterModel;

            if (registerModel != null)
            {
                var user = new ApplicationUser
                {
                    UserName = registerModel.UserName,
                    FullName = registerModel.FullName,
                    Email = registerModel.Email,
                    PhoneNumber = registerModel.PhoneNumber
                };

                if (!await _roleManager.RoleExistsAsync(registerModel.RoleName))
                {
                    return false;
                };

                var userResult = await _userManager.CreateAsync(user, registerModel.Password);

                if (userResult.Succeeded)
                {
                    // Assign the user to the specified role
                    var roleResult = await _userManager.AddToRoleAsync(user, registerModel.RoleName);

                    // Create a bank account for the user
                    await _userAccountRepository.InsertAsync(new UserAccount
                    {
                        UserId = user.Id
                    });
                    var accountResult = await _userAccountRepository.SaveAsync();

                    if (!roleResult.Succeeded || accountResult < 1)
                    {
                        // If role assignment fails, you might want to delete the user
                        await _userManager.DeleteAsync(user);

                        return false;
                    }

                    return true;
                }
            }

            return false;
        }


        private async Task<ApplicationUser?> Validate(LoginModel model)
        {
            var result = _signInManager.PasswordSignInAsync(model.Username, model.Password, false, false);

            if (result.Result.Succeeded)
            {
                var user = await _userManager.FindByNameAsync(model.Username);
                if (user != null)
                {
                    return user;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }


        private Tuple<string, string> GenerateJwtToken(ApplicationUser user)
        {
            var secretKey = _jwtOptions.SecretKey;
            var key = Encoding.ASCII.GetBytes(secretKey);

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName!),
                new Claim(ClaimTypes.Email, user.Email!),
                new Claim("userId", user.Id!),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var roles = _userManager.GetRolesAsync(user).Result;
            foreach (var role in roles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, role));
            }

            var token = new JwtSecurityToken(
                issuer: _jwtOptions.ValidIssuer,
                audience: _jwtOptions.ValidAudience,
                claims: authClaims,
                expires: DateTime.UtcNow.AddMinutes(5),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature));


            return Tuple.Create(new JwtSecurityTokenHandler().WriteToken(token), GenerateRefreshToken());
        }


        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }
}
