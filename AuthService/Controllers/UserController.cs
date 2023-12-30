
using AuthService.Models.Dtos;
using AuthService.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUser _userService;
        private readonly ResponseDto _response;

        public UserController(IUser user)
        {
            _userService = user;
            _response = new ResponseDto();
        }
        [HttpPost("register")]
        public async Task<ActionResult<ResponseDto>> RegisterUser(RegisterUserDto registerUserDto)
        {
            var res = await _userService.RegisterUser(registerUserDto);
            if(string.IsNullOrWhiteSpace(res))
            {
                //success
                _response.Result = "User Registered successfully";
                return Created("", _response);
            }
            _response.ErrorMessage = res;
            _response.IsSuccess = false;
            return BadRequest(_response);
        }
        [HttpPost("login")]
        public async Task<ActionResult<ResponseDto>> loginUser(LoginRequestDto loginRequestDto)
        {
            var res = await _userService.loginUser(loginRequestDto);

            if (res.User!=null)
            {
                _response.Result = res;
                return Created("", _response);
            }

            _response.ErrorMessage = "Invalid Credentials";
            _response.IsSuccess = false;
            return BadRequest(_response);

        }
    }
}
