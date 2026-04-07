using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToeicMockTest.Domain.Common;
using ToeicMockTest.Domain.Common.Enums;

namespace ToeicMockTest.Domain.Entities
{
    public class ExamAttempt : BaseEntity
    {
        public Guid ExamId { get; private set; }
        public Guid UserId { get; private set; }

        public Exam Exam { get; private set; } = null!;
        public User User { get; private set; } = null!;

        public DateTime StartedAt { get; private set; }
        public DateTime? SubmittedAt { get; private set; }

        public float TotalScore { get; private set; }
        public float ListeningScore { get; private set; }
        public float ReadingScore { get; private set; }

        public int CorrectCount { get; private set; }
        public int WrongCount { get; private set; }
        public int SkippedCount { get; private set; }

        public AttemptStatus Status { get; private set; }

        private readonly List<UserAnswer> _userAnswers = new();
        public IReadOnlyCollection<UserAnswer> UserAnswers => _userAnswers;

        private ExamAttempt() { }

        public ExamAttempt(Guid examId, Guid userId)
        {
            ExamId = examId;
            UserId = userId;
            StartedAt = DateTime.UtcNow;
            Status = AttemptStatus.InProgress;
        }

        public void Submit(float listeningScore, float readingScore, int correct, int wrong, int skipped)
        {
            SubmittedAt = DateTime.UtcNow;
            ListeningScore = listeningScore;
            ReadingScore = readingScore;
            TotalScore = listeningScore + readingScore;
            CorrectCount = correct;
            WrongCount = wrong;
            SkippedCount = skipped;
            Status = AttemptStatus.Completed;
        }
    }
}

