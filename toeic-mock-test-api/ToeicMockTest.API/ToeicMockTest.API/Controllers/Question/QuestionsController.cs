using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ToeicMockTest.Application.Services.Questions;
using ToeicMockTest.Contracts.Questions.Requests;
using ToeicMockTest.Contracts.Questions.Responses;

namespace ToeicMockTest.API.Controllers.Question
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuestionsController : Controller
    {
        private readonly IQuestionService _questionService;

        public QuestionsController(IQuestionService questionService)
        {
            _questionService = questionService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<QuestionResponse>>> GetAll(CancellationToken ct)
        {
            var questions = await _questionService.GetAllAsync(ct);
            return Ok(questions);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<QuestionResponse>> GetById(Guid id, CancellationToken ct)
        {
            var question = await _questionService.GetByIdAsync(id, ct);
            if (question == null)
            {
                return NotFound(new { Message = "Không tìm thấy câu hỏi." });
            }
            return Ok(question);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateQuestionRequest request, CancellationToken ct)
        {
            var userId = GetCurrentUserId();
            var questionId = await _questionService.CreateAsync(request, userId, ct);

            return CreatedAtAction(nameof(GetById), new { id = questionId }, questionId);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateQuestionRequest request, CancellationToken ct)
        {
            try
            {
                var userId = GetCurrentUserId();
                await _questionService.UpdateAsync(id, request, userId, ct);
                return NoContent(); // Trả về 204 nếu cập nhật thành công
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
        {
            await _questionService.DeleteAsync(id, ct);
            return NoContent();
        }

        /// <summary>
        /// Hàm helper lấy UserId từ Claims của JWT Token
        /// </summary>
        private Guid GetCurrentUserId()
        {
            // Lấy ID từ Claim NameIdentifier (thường dùng trong JWT)
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userIdClaim))
            {
                // Nếu chưa có hệ thống Auth, bạn có thể tạm thời trả về một Guid cố định để test
                // return Guid.Parse("00000000-0000-0000-0000-000000000000");

                // Sau này khi chạy thật, nếu ko có userId thì throw lỗi
                throw new UnauthorizedAccessException("Người dùng chưa đăng nhập.");
            }

            return Guid.Parse(userIdClaim);
        }
    }
}
