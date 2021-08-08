using AuthenticationService.API.Utility;
using AuthenticationService.Models;
using AuthenticationService.Service;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationService.Controllers
{
    [Route("auth")]
    [ExceptionHandler]
    [LogHandler]
    public class AuthController : Controller
    {
        private readonly ITokenGenerator tokenGenerator;
        private readonly IAuthService service;
        private readonly ITokenValidator tokenValidator;

        public AuthController(IAuthService _service, ITokenGenerator _tokenGenerator, ITokenValidator _tokenValidator)
        {
            this.service = _service;
            this.tokenGenerator = _tokenGenerator;
            this.tokenValidator = _tokenValidator;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Action Executed");
        }

        // POST api/<controller>
        [HttpPost]
        [Route("register")]
        public IActionResult Register([FromBody]User user)
        {
            bool result = false;
            try
            {
                result = service.RegisterUser(user);
            }
            catch (Exceptions.UserNotCreatedException)
            {
                return Conflict();
            }

            return Created("", result);
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login([FromBody]User user)
        {
            User result;
            try
            {
                var userName = user.UserId;
                var password = user.Password;
                result = service.LoginUser(userName, password);


                string value = tokenGenerator.GetJWTToken(user.UserId);
                return Ok(value);
            }
            catch (Exceptions.UserNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPost]
        [Route("validate")]
        public IActionResult Validate([FromBody]AuthToken token)
        {
            var result = tokenValidator.ValidateToken(token);
            return Ok(result);
        }
    }
}
