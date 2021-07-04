using DogBarberShopProject.Interfaces;
using DogBarberShopProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DogBarberShopProject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication;

namespace DogBarberShopProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class QueueManagementController : ControllerBase
    {
        private IQueueManagement _queueManagement;
        private DataContext context;
        private IAuthentication _authentication;
        public QueueManagementController(IQueueManagement queueManagement,DataContext dc,IAuthentication ia)
        {
            context = dc;
            _queueManagement = queueManagement;
            _authentication = ia;
        }

        [HttpGet]   
        public async Task<IActionResult> GetQueue()
        {
            var Queues = _queueManagement.getQueues();
            return Ok(Queues);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQueue(int id)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");

           if (_queueManagement.deleteQueue(id,accessToken))
               return Ok(new Response<string>() { Status="200",Result= "Queue removed." });
           return BadRequest(new Response<string>() { Status = "500", Result = "Queue not removed" });
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateQueue([FromBody] UpdateQueueModel updateQueueModel)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            if(_queueManagement.updateQueue(updateQueueModel, accessToken))
                return Ok();
            return BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult> AddQueue([FromBody] AddQueueModel addQueueModel)
        {
            //context.queues.FromSqlRaw("EXEC [dbo].[Addappointment] 1, '2016-10-23 20:45:37.0000000'");
            //context.SaveChanges();
            _queueManagement.addQueue(addQueueModel);
            return Ok(addQueueModel);
        }

    }
}
