using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToeicMockTest.Domain.Common;

namespace ToeicMockTest.Domain.Entities
{
    public class UserAnswer : BaseEntity
    {
        public Guid ExamAttemptId { get; private set; }
        public ExamAttempt ExamAttempt { get; private set; } = null!;

        public Guid QuestionId { get; private set; }
        public Question Question { get; private set; } = null!;

        public Guid? SelectedAnswerId { get; private set; }
        public Answer? SelectedAnswer { get; private set; }

        public bool IsCorrect { get; private set; }

        private UserAnswer() { }

        public UserAnswer(Guid examAttemptId, Guid questionId, Guid? selectedAnswerId, bool isCorrect)
        {
            ExamAttemptId = examAttemptId;
            QuestionId = questionId;
            SelectedAnswerId = selectedAnswerId;
            IsCorrect = isCorrect;
        }
    }
}
