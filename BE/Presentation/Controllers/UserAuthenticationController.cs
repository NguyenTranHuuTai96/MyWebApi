using FluentValidation;
using IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ViewModels;
using ViewModels.DataBase;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("UserAuthentication")]
    [Produces("text/plain")]
    public class UserAuthenticationController : ControllerBase
    {
        IValidator<AccountModel> _validator;
        IUserIdentityServices _userIdentityServices;
        ITokenIdentityServices _tokenIdentityServices;
        public UserAuthenticationController(
             IValidator<AccountModel> validator,
             IUserIdentityServices userIdentityServices,
             ITokenIdentityServices tokenIdentityServices
             ) {
            _validator = validator;
            _userIdentityServices = userIdentityServices;
            _tokenIdentityServices = tokenIdentityServices;
        }
        [HttpPost("login-user-identity")]
        [AllowAnonymous]
        public async Task<IActionResult> Login( [FromBody] AccountModel accountModel)
        {
            var validations = await _validator.ValidateAsync(accountModel);

            if (!validations.IsValid)
            {
                return BadRequest(JsonSerializer.Serialize(validations.Errors.Select(x => new
                {
                    FieldName = x.PropertyName,
                    Error = x.ErrorMessage
                })));
            }
            JwtModel jwtModel = await _tokenIdentityServices.CreateJwtModel(accountModel);
            if (jwtModel is null)   return Unauthorized("Account is not wrong or is not active");
            return Ok(JsonSerializer.Serialize(jwtModel));
        }
        [HttpPost("refresh-token-identity")]
        public async Task<IActionResult> RefreshToken([FromBody] JwtModel token)
        {
            if (token == null) return BadRequest("Could not get refresh token");
            var rs = await _tokenIdentityServices.ValidateRefreshToken(token.RefreshToken);
            return Ok(JsonSerializer.Serialize(rs));
        }
    }
}
