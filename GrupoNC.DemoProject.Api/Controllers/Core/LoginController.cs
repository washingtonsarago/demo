namespace AqueleEsquema.Api.Controllers
{
    using GrupoNC.DemoProject.Api.Models;
    using GrupoNC.DemoProject.Api.Services.Interfaces;
    using GrupoNC.DemoProject.Api.Utilities;
    using GrupoNC.DemoProject.Api.Utilities.Extensions;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Options;
    using System;
    using System.Linq;
    using System.Net.Mail;
    using System.Threading.Tasks;

    public class LoginController : ControllerBase
    {
        private readonly int _LinkExpirationMinutes = 10;
        private readonly IOptions<JWTModel> _JwtConfig;
        private readonly IUsersService _usersService;

        public LoginController(
            IUsersService usersService,
            IOptions<JWTModel> jwtConfig
        )
        {
            _usersService = usersService;
            _JwtConfig = jwtConfig;
        }

        #region GET

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [RequestFormLimits(ValueLengthLimit = int.MaxValue, MultipartBodyLengthLimit = int.MaxValue)]
        [DisableRequestSizeLimit]
        [Route("Login")]
        public async Task<ActionResult> Login(
            [FromHeader] string? login, 
            [FromHeader] string password)
        {
            var internalLogin = login;
            var encryptedPassword = password.ToMD5();
            var list = await _usersService.Query(u => u.IsActive == true && u.Login == internalLogin && u.Password == encryptedPassword);

            if (list?.Count() == 1)
            {
                var loggedUser = list.First();
                
                return new OkObjectResult(new { } /* Aqui gera o token */);
            }
            else
                return new UnauthorizedResult();
        }

        #endregion GET

        #region POST

        [HttpPost]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [RequestFormLimits(ValueLengthLimit = int.MaxValue, MultipartBodyLengthLimit = int.MaxValue)]
        [DisableRequestSizeLimit]
        [Route("SendFirstPasswordLink")]
        public async Task<IActionResult> SendFirstPasswordLink([FromHeader] string? login)
        {
            return Ok();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status428PreconditionRequired)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [RequestFormLimits(ValueLengthLimit = int.MaxValue, MultipartBodyLengthLimit = int.MaxValue)]
        [DisableRequestSizeLimit]
        [Route("SendForgotPasswordLink")]
        public async Task<IActionResult> SendForgotPasswordLink([FromHeader] string email)
        {
            try
            {
                return new OkResult();
            }
            catch
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        

        #endregion POST
    }
}