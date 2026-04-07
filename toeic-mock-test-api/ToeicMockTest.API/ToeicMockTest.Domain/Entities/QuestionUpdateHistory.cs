using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToeicMockTest.Domain.Common;

namespace ToeicMockTest.Domain.Entities
{
    public class QuestionUpdateHistory : BaseEntity
    {
        public Guid QuestionId { get; private set; }
        public Question Question { get; private set; } = null!;

        public Guid UpdatedById { get; private set; }
        public User UpdatedBy { get; private set; } = null!;

        public DateTime UpdatedDate { get; private set; } = DateTime.UtcNow;

        public string? OldContent { get; private set; }
        public string? NewContent { get; private set; }

        private QuestionUpdateHistory() { }

        public QuestionUpdateHistory(
            Guid questionId,
            Guid updatedById,
            string? oldContent,
            string? newContent)
        {
            QuestionId = questionId;
            UpdatedById = updatedById;
            OldContent = oldContent;
            NewContent = newContent;
        }
    }
}
