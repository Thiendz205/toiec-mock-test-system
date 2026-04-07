using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToeicMockTest.Domain.Common;

namespace ToeicMockTest.Domain.Entities
{
    public class UserGlobalStat : BaseAuditableEntity
    {
        public Guid UserId { get; private set; }
        public int TotalExamsTaken { get; private set; }
        public int TotalQuestionsSolved { get; private set; }
        public float AverageScore { get; private set; }
        public float HighestScore { get; private set; }
        public int StreakDays { get; private set; }

        public User User { get; private set; } = null!;

        private UserGlobalStat() { }

        public UserGlobalStat(Guid userId)
        {
            UserId = userId;
            TotalExamsTaken = 0;
            TotalQuestionsSolved = 0;
            AverageScore = 0;
            HighestScore = 0;
            StreakDays = 0;
        }
        public void UpdateStats(int questionsSolved, float score)
        {
            float totalScore = (AverageScore * TotalExamsTaken) + score;
            TotalExamsTaken++;
            TotalQuestionsSolved += questionsSolved;
            AverageScore = totalScore / TotalExamsTaken;

            if (score > HighestScore)
            {
                HighestScore = score;
            }
        }

        public void UpdateStreak(int days)
        {
            StreakDays = days;
        }
    }
}
