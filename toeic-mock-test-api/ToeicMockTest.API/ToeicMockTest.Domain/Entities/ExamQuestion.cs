using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToeicMockTest.Domain.Common;

namespace ToeicMockTest.Domain.Entities
{
    public class ExamQuestion : BaseEntity
    {
        public Guid ExamId { get; private set; }
        public Exam Exam { get; private set; } = null!;

        public Guid QuestionId { get; private set; }
        public Question Question { get; private set; } = null!;

        public int Order { get; private set; }
        public float Score { get; private set; }

        private ExamQuestion() { }

        public ExamQuestion(Guid examId, Guid questionId, int order, float score)
        {
            ExamId = examId;
            QuestionId = questionId;
            Order = order;
            Score = score;
        }
    }
}
