using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Polonicus_API.Entities;
using Polonicus_API.Exceptions;
using Polonicus_API.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Polonicus_API.Services
{
    public interface IAccountService
    {
        void RegisterUserDto(RegisterUserDto dto);
        public string GetToken(LoginDto dto);
    }
    public class AccountService : IAccountService
    {
        private readonly PolonicusDbContext dbContext;
        private readonly IPasswordHasher<User> passwordHasher;
        private readonly AuthenticationSettings authentication;

        public AccountService(PolonicusDbContext _dbContext, IPasswordHasher<User> _passwordHasher, AuthenticationSettings _authentication)
        {
            dbContext = _dbContext;
            passwordHasher = _passwordHasher;
            authentication = _authentication;
        }


        public void RegisterUserDto(RegisterUserDto dto)
        {
            var newUser = new User()
            {
                Email = dto.Email,
                DateOfBirth = dto.DateOfBirth,
                Nationality = dto.Nationality,
                RoleId = dto.RoleId,
                FirstName=dto.FirstName,
                LastName=dto.LastName,
            };
            var hashedPassword = passwordHasher.HashPassword(newUser, dto.Password);
            newUser.PasswordHash = hashedPassword;

            dbContext.Users.Add(newUser);
            dbContext.SaveChanges();
        }

        public string GetToken(LoginDto dto)
        {
            var user = dbContext
                .Users
                .Include(u=>u.Role)
                .FirstOrDefault(u => u.Email == dto.Email);

            if (user is null) throw new BadRequestException("Invalid password or email adress");

            var result = passwordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);

            if(result == PasswordVerificationResult.Failed)
            {
                throw new BadRequestException("Invalid password or email adress");
            }

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(ClaimTypes.Name,$"{user.FirstName}, {user.LastName}"),
                new Claim(ClaimTypes.Role,user.Role.Name),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authentication.JwtKey));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiration = DateTime.Now.AddDays(authentication.JwtExpirationDays);

            var token = new JwtSecurityToken(
                authentication.JwtIssuer,
                authentication.JwtIssuer,
                claims,
                expires: expiration,
                signingCredentials: credentials
                );

            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }
    }
}
