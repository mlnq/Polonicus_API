using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Polonicus_API.Models;
using Polonicus_API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Polonicus_API.Controllers
{

    [Route("/api/account")]
    [ApiController]
    [AllowAnonymous]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService accountService;

        public AccountController(IAccountService _accountService)
        {
            accountService = _accountService;
        }

        [HttpPost("register")]
        public ActionResult RegisterUser([FromBody] RegisterUserDto dto)
        {
            accountService.RegisterUserDto(dto);
            return Ok();
        }

        [HttpPost("login")]
        public ActionResult Login([FromBody] LoginDto dto)
        {
            UserDto loggedUser = accountService.LoginUser(dto);
           //string token = accountService.GetToken(dto);
           // return Ok(token);
            return Ok(loggedUser);

        }

        [HttpGet]
        public ActionResult GetLoggedInUser()
        {
            UserDto loggedUser = accountService.GetLoggedInUser(HttpContext.User);
            return Ok(loggedUser);
        }
    }
}
