using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToeicMockTest.Domain.Repositories.Questions;
using ToeicMockTest.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ToeicMockTest.Infrastructure.Persistence.Repositories.Questions
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly ApplicationDbContext _context;
        public QuestionRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Question> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            return await _context.Questions.FindAsync(new object[] { id }, ct);
        }
        public async Task<Question> GetWithDetailsAsync(Guid id, CancellationToken ct = default)
        {
            return await _context.Questions
                .Include(q => q.Answers)
                .Include(q => q.Difficulty)
                .Include(q => q.QuestionTags)
                .Include(q => q.QuestionGroup)
                .FirstOrDefaultAsync(q => q.Id == id, ct);

        }
        public async Task<IEnumerable<Question>> GetAllAsync(CancellationToken ct = default)
        { 
            return await _context.Questions
                .Include(q => q.Difficulty)
                .ToListAsync(ct);
        }
        public async Task AddAsync(Question question, CancellationToken ct = default)
        { 
            await _context.Questions.AddAsync(question, ct);
        }
        public void Update(Question question)
        {
            _context.Update(question);
        }
        public void Delete(Question question)
        {
            _context.Remove(question);
        }
    }
}
