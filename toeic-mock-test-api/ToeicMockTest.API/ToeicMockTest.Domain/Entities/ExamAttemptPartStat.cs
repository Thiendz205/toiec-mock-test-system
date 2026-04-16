using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToeicMockTest.Domain.Common;
using ToeicMockTest.SharedKernel.Common.Enums;

namespace ToeicMockTest.Domain.Entities
{
    public class ExamAttemptPartStat : BaseEntity
    {
        public Guid ExamAttemptId { get; private set; }
        public ToeicPart Part { get; private set; }
        public int CorrectCount { get; private set; }
        public ExamAttempt ExamAttempt { get; private set; } = null!;

        private ExamAttemptPartStat() { }

        public ExamAttemptPartStat(Guid examAttemptId, ToeicPart part, int correctCount)
        {
            ExamAttemptId = examAttemptId;
            Part = part;
            CorrectCount = correctCount;
        }
    }
}
