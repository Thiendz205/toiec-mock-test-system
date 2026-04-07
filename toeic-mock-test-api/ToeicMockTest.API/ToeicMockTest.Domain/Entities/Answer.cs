using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToeicMockTest.Domain.Common;

namespace ToeicMockTest.Domain.Entities
{
    public class Answer : BaseEntity
    {
        public string Content { get; private set; }
        public bool IsCorrect { get; private set; }
        public int Order { get; private set; }
        public Guid QuestionId { get; private set; }
        public Question Question { get; private set; } = null!;

        private Answer() { }

        public Answer(string content, bool isCorrect, int order, Guid questionId)
        {
            Content = content;
            IsCorrect = isCorrect;
            Order = order;
            QuestionId = questionId;
        }

        public void Update(string content, bool isCorrect, int order)
        {
            Content = content;
            IsCorrect = isCorrect;
            Order = order;
        }
    }
}
