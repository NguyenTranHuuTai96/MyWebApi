﻿using IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ViewModels;
using ViewModels.DataBase;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("UserIdentity")]
    [Produces("text/plain")]
    public class UserIdentityController : ControllerBase
    {
        public IUserIdentityServices _userIdentityServices;
        public UserIdentityController(
            IUserIdentityServices userIdentityServices
            )
        {
            _userIdentityServices = userIdentityServices;
        }

        [HttpPost]
        [Route("register-user")]
        public async Task<IActionResult> RegisterUser( [FromBody] UserIdentityModel userIdentityModel, CancellationToken cancellationToken)
        {
            try
            {
                if (userIdentityModel is null) return BadRequest("Invalid Data");
                var rs = await _userIdentityServices.RegisterUserIdentityServices(userIdentityModel);
                if (!rs.Succeeded) return BadRequest();

                await _userIdentityServices.AddRoles(userIdentityModel);

                //Send Email for Confirm
                ModelConfirmMail modelConfirmMail = await _userIdentityServices.SetDataModelConfirmMail(userIdentityModel);
                string url = Url.Action("ConfirmEmail", "UserIdentity", modelConfirmMail, Request.Scheme)??string.Empty;
                await _userIdentityServices.SendMailForUserConfirm(userIdentityModel, url, cancellationToken);
                return Ok(JsonSerializer.Serialize("OK"));
            }
            catch (Exception ex) {
                return BadRequest(ex.ToString());
            }      

        }

        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfirmEmail(string IdUser , string TokenConfirmMail)
        {

            var identityResult = await _userIdentityServices.UpdateConfirmMail(IdUser, TokenConfirmMail);

            if (!identityResult.Succeeded) return BadRequest("Error");
          
            return Ok("Your account has been actived");
            
            
        }

    }
}
