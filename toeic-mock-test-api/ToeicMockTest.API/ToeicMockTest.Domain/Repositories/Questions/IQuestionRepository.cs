using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToeicMockTest.Domain.Entities;

namespace ToeicMockTest.Domain.Repositories.Questions
{
    public interface IQuestionRepository
    {
        // Các hàm đọc (Async)
        Task<Question> GetByIdAsync(Guid id, CancellationToken ct = default);
        Task<Question> GetWithDetailsAsync(Guid id, CancellationToken ct = default);
        Task<IEnumerable<Question>> GetAllAsync(CancellationToken ct = default);

        Task AddAsync(Question question, CancellationToken ct = default);
        void Update(Question question);
        void Delete(Question question);
    }
}
