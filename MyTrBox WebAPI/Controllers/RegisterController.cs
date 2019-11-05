using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyTrBox_WebAPI.Interfaces;
using MyTrBox_WebAPI.Model;
using static MyTrBox_WebAPI.ModelViewHolder.AccountViewHolder;

namespace MyTrBox_WebAPI.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {

        private readonly IAccount _account;

        public RegisterController(IAccount account)
        {
            _account = account;
        }

        [HttpPost(Name = nameof(RegisterUser))]
        public async Task<IActionResult> RegisterUser([FromBody] UserViewModel model)
        {
            var (succeed, message) = await _account.CreatUserAsync(model);
            if (succeed) return Created("success",
                new {message = "User created" }
                );

            return BadRequest(
                new ApiError {
                    Message = "Registration Failed",
                    Detail = message
                }
                );
        }
    }
}