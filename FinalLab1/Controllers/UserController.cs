using System.Security.Claims;
using FinalLab1.Entities;
using FinalLab1.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;

namespace FinalLab1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : GenericControllerBase<User,int>
    {
        private readonly IGenericService<User, int> userService;

        public UserController(IGenericService<User,int> service,IGenericService<User, int> userService) : base(service)
        {
            this.userService = userService;
        }
        [Authorize]
        [HttpGet("me")]
        public async Task<IActionResult> GetCurrentUserInfo()
        {
            var userIdStr = User.FindFirstValue(JwtRegisteredClaimNames.Sub) ?? User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!int.TryParse(userIdStr, out int userId))
                return Unauthorized("Invalid token");

            var user = await userService.GetByIdAsync(Convert.ToInt32(userIdStr));
            if (user == null)
                return NotFound("User not found");

            return Ok(new
            {
                user.Id,
                user.Name,
                user.Email,
                user.Role,
                user.Birthday,
                user.Gender
            });
        }
    }
}
