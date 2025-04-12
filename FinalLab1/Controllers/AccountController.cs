using FinalLab1.Converter;
using FinalLab1.Dtos;
using FinalLab1.Entities;
using FinalLab1.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Serialization;

namespace FinalLab1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IGenericService<User, int> userService;
        private readonly JwtTokenGenerator jwtBearerExtensions;
        public AccountController(IGenericService<User, int> userService, JwtTokenGenerator jwtBearerExtensions)
        {
            this.userService = userService;
            this.jwtBearerExtensions = jwtBearerExtensions;
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var user = await userService.FindByConditionAsync(u => u.Email == dto.Email && u.Password == dto.Password);
            Console.WriteLine(user.Count());

            if (user.Count()== 0)
                return Unauthorized("Invalid email or password");
            var Finaluser = user.FirstOrDefault();
            

            var token = jwtBearerExtensions.GenerateToken( Finaluser.Id, Finaluser.Name, Finaluser.Role);

            return Ok(new
            {
                token,
                user = new { Finaluser.Id, Finaluser.Name, Finaluser.Role }
            });
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            var find = await userService.FindByConditionAsync(u => u.Email == dto.Email);
            if (find.Count()!=0 )
                return BadRequest("Email already exists.");

            var user = new User
            {
                Name = dto.Name,
                Email = dto.Email,
                Phone = dto.Phone,
                Password = dto.Password, // ❗ Chưa mã hoá password
                Birthday = dto.Birthday,
                Gender = dto.Gender,
                Role = Role.User
            };

             var result =await userService.AddAsync(user);
            if (result==null)
            {
                return BadRequest("failed to register user");
            }
            
            return Ok(new 
            {
                Id = user.Id,
                Name = user.Name,
                Role = user.Role
            });
        }
    }
}
