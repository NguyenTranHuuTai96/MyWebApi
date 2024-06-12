
using FluentValidation;
using IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ViewModels;
using ViewModels.DataBase;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("Authentication")]
    [Produces("text/plain")]
    public class AuthenticationController : ControllerBase
    {
        public IUserServices _userServices;
        public ITokenServices _tokenServices;
     
        public AuthenticationController(IUserServices userServices,
            ITokenServices tokenServices
       
            )
        {
            _userServices = userServices;
            _tokenServices = tokenServices;
          

        }
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] AccountModel model)
        {
            if (model == null) return BadRequest();
            var userModelCheck = _userServices.CheckLogin(model.UserName,model.Password);

            if (userModelCheck == null) return Unauthorized();
            string dataAccessToken = await _tokenServices.CreateTokenOptimize(userModelCheck);
            string refreshToken = await _tokenServices.CreateRefreshTokenOptimize(userModelCheck);

            UserTokenModel userTokenModel = new UserTokenModel();
            userTokenModel.AccessToken = dataAccessToken;
            userTokenModel.RefreshToken = refreshToken;
            userTokenModel.UserId = userModelCheck.Id;
            await _tokenServices.SaveToken(userTokenModel);


            return Ok(JsonSerializer.Serialize(new JwtModel
            {
                AccessToken = dataAccessToken,
                RefreshToken = refreshToken,
            }));
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] JwtModel token)
        {
            if (token == null)
                return BadRequest("Could not get refresh token");
            var rs = await _tokenServices.ValidateRefreshToken(token.RefreshToken);
            return Ok(JsonSerializer.Serialize(rs));
        }

    }
}
