using DogBarberShopProject.Interfaces;
using DogBarberShopProject.Models;
using DogBarberShopProject.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DogBarberShopProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private IAuthentication _userAuthentication;
        private DataContext context;

        public AuthenticationController(IAuthentication ia,DataContext dc)
        {
            _userAuthentication = ia;
            context = dc;
        }

        [HttpPut]
        public async Task<IActionResult> SignUp([FromBody] User user)
        {
            try
            {
                if(_userAuthentication.IsUserExist(user.UserName))
                {
                    return BadRequest(new Response<string> { Status = "501", Result = "user exist" });
                }
                _userAuthentication.AddUser(user);
                return Ok(new Response<string> { Status = "200", Result = "user created." });
            }
            catch(Exception ex)
            {
                throw new Exception();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginModel userLoginModel)
        {
            try
            {
                if (!_userAuthentication.IsUserExist(userLoginModel.UserName))
                {
                    return BadRequest(new Response<string> { Status = "501", Result = "no user exist" });
                }
               
                if(_userAuthentication.VerifyPassword(userLoginModel))
                {
                    var user = context.users.SingleOrDefault(u => u.UserName == userLoginModel.UserName);
                    string token = _userAuthentication.CreateToken(user.UserName);
                    return Ok(new Response<LoginModelResponse> { Status = "200", Result = new LoginModelResponse { Name = user.Name,Token = token,UserId = user.Id } });
                }   
                else
                    return BadRequest(new Response<string> { Status = "501", Result = "wrong password." });
            }

            catch (Exception ex)
            {
                throw new Exception();
            }
        }
    }
}
