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
            string token = accountService.GetToken(dto);
            return Ok(token);
        }
    }
}
