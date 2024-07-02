using Billing.Management.Application.Auth.JwtHelper.Interface;
using Billing.Management.Domain.Auth.Model;
using Billing.Management.Domain.Auth.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Billing.Management.Api.Controllers.Auth
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IJwtAuth? _jwtAuth;
        private readonly IUserAuthRegister? _userAuthRegister;

        public AuthController(IJwtAuth? jwtAuth, IUserAuthRegister? userAuthRegister)
        {
            _jwtAuth = jwtAuth;
            _userAuthRegister = userAuthRegister;
        }

        /// <summary>
        /// Register a new user
        /// </summary>
        /// <param name="user">Detalhes do usuário sendo registrado</param>
        /// <returns>Returns if an user was registered or not</returns>
        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([FromBody] UserAuth user)
        {
            if (user is UserAuth)
            {
                await _userAuthRegister.CreateAsync(user);

                return Created
                (
                    "Created", 
                    new 
                    {
                        ResponseCode = StatusCodes.Status201Created,
                        ResponseMessage = "User successfully created."                        
                    }
                );
            }

            return BadRequest("Erro ao registrar usuário.");
        }

        /// <summary>
        /// Authenticates a user
        /// </summary>
        /// <param name="email">Email of the user.</param>
        /// <param name="password">Password of the user.</param>
        /// <returns>Token JWT if success</returns>
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login(string email, string password)
        {
            var userExists = await _userAuthRegister.GetAsync(email.ToLower());

            if (userExists == null)
                return Unauthorized(new { Message = "Email e/ou senha inválido(s)." });


            if (userExists.Password != password)
                return Unauthorized(new { Message = "Email e/ou senha inválido(s)." });


            var token = _jwtAuth.GenerateToken(userExists);

            return Ok(new { token });
        }
    }
}
