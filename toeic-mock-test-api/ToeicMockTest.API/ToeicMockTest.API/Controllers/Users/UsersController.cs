using Microsoft.AspNetCore.Mvc;
using ToeicMockTest.Application.Services.Users;
using ToeicMockTest.Contracts.Users.Requests;
using ToeicMockTest.Contracts.Users.Responses;
using ToeicMockTest.SharedKernel.Common.Enums;
namespace ToeicMockTest.API.Controllers.Users
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(UserResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateUserRequest request)
        {
            try
            {
                var adminId = Guid.Empty; // Sau này sẽ lấy từ JWT Token
                var result = await _userService.CreateUserAsync(request, adminId);
                return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<UserResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll([FromQuery] RecordStatus? status, CancellationToken ct)
        {
            var users = await _userService.GetAllUsersAsync(status, ct);
            return Ok(users);
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null) return NotFound(new { message = "User not found" });
            return Ok(user);
        }

        [HttpPut("admin-update")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AdminUpdate([FromBody] UpdateUserRequest request)
        {
            try
            {
                var adminId = Guid.Empty;
                var result = await _userService.AdminUpdateUserAsync(request, adminId);
                if (!result) return BadRequest(new { message = "Update failed" });

                return NoContent(); // Hoặc Ok()
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id:guid}/lock")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Lock(Guid id, CancellationToken ct)
        {
            try
            {
                var adminId = Guid.Empty;
                await _userService.LockUserAsync(id, adminId, ct);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id:guid}/unlock")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Unlock(Guid id, CancellationToken ct)
        {
            try
            {
                var adminId = Guid.Empty;
                await _userService.UnlockUserAsync(id, adminId, ct);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var adminId = Guid.Empty;
            var result = await _userService.DeleteUserAsync(id, adminId);
            return result ? NoContent() : NotFound(new { message = "User not found" });
        }
        [HttpPut("{id:guid}/restore")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Restore(Guid id, CancellationToken ct)
        {
            try
            {
                var adminId = Guid.Empty;
                await _userService.RestoreUserAsync(id, adminId, ct);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
