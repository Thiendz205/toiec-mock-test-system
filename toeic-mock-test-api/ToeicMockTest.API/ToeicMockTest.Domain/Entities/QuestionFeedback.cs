using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToeicMockTest.Domain.Common;

namespace ToeicMockTest.Domain.Entities
{
    public class QuestionFeedback : BaseEntity
    {
        public Guid QuestionId { get; private set; }
        public Question Question { get; private set; } = null!;

        public Guid UserId { get; private set; }
        public User User { get; private set; } = null!;

        public string Content { get; private set; }
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
        public bool IsResolved { get; private set; }

        private QuestionFeedback() { }

        public QuestionFeedback(Guid questionId, Guid userId, string content)
        {
            QuestionId = questionId;
            UserId = userId;
            Content = content;
            IsResolved = false;
        }

        public void MarkAsResolved() 
        { 
            IsResolved = true;
        }
    }
}
