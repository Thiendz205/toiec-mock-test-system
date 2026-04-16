using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToeicMockTest.Contracts.Questions.Requests;
using ToeicMockTest.Contracts.Questions.Responses;

namespace ToeicMockTest.Application.Services.Questions
{
    public interface IQuestionService
    {
        Task<QuestionResponse?> GetByIdAsync(Guid id, CancellationToken ct = default);
        Task<IEnumerable<QuestionResponse>> GetAllAsync(CancellationToken ct = default);
        Task<Guid> CreateAsync(CreateQuestionRequest request, Guid currentUserId, CancellationToken ct = default);
        Task UpdateAsync(Guid id, UpdateQuestionRequest request, Guid currentUserId, CancellationToken ct = default);
        Task DeleteAsync(Guid id, CancellationToken ct = default);
    }
}
