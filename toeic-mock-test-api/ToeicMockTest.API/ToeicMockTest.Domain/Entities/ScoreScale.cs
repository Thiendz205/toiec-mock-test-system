using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToeicMockTest.Domain.Common;

namespace ToeicMockTest.Domain.Entities
{
    public class ScoreScale : BaseEntity
    {
        public Guid ExamId { get; private set; }
        public int CorrectCount { get; private set; }
        public int ListeningScore { get; private set; }
        public int ReadingScore { get; private set; }

        public Exam Exam { get; private set; } = null!;

        private ScoreScale() { }

        public ScoreScale(Guid examId, int correctCount, int listeningScore, int readingScore)
        {
            ExamId = examId;
            CorrectCount = correctCount;
            ListeningScore = listeningScore;
            ReadingScore = readingScore;
        }
    }
}
