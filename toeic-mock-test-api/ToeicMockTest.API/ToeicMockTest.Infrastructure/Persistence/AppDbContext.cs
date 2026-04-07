using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ToeicMockTest.Domain.Common;
using ToeicMockTest.Domain.Entities;

namespace ToeicMockTest.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
       : base(options)
        {
        }

        #region --- User & Auth ---
        public DbSet<User> Users => Set<User>();
        public DbSet<Role> Roles => Set<Role>();
        public DbSet<UserVerificationToken> UserVerificationTokens => Set<UserVerificationToken>();
        public DbSet<UserGlobalStat> UserGlobalStats => Set<UserGlobalStat>();
        #endregion

        #region --- Exams & Results ---
        public DbSet<Exam> Exams => Set<Exam>();
        public DbSet<ExamCollection> ExamCollections => Set<ExamCollection>();
        public DbSet<ExamQuestion> ExamQuestions => Set<ExamQuestion>();
        public DbSet<ExamAttempt> ExamAttempts => Set<ExamAttempt>();
        public DbSet<ExamAttemptPartStat> ExamAttemptPartStats => Set<ExamAttemptPartStat>();
        public DbSet<ScoreScale> ScoreScales => Set<ScoreScale>();
        public DbSet<UserAnswer> UserAnswers => Set<UserAnswer>();
        #endregion

        #region --- Questions Content ---
        public DbSet<Question> Questions => Set<Question>();
        public DbSet<QuestionGroup> QuestionGroups => Set<QuestionGroup>();
        public DbSet<Answer> Answers => Set<Answer>();
        public DbSet<Difficulty> Difficulties => Set<Difficulty>();
        public DbSet<Tag> Tags => Set<Tag>();
        public DbSet<QuestionTag> QuestionTags => Set<QuestionTag>();
        public DbSet<QuestionFeedback> QuestionFeedbacks => Set<QuestionFeedback>();
        public DbSet<QuestionUpdateHistory> QuestionUpdateHistories => Set<QuestionUpdateHistory>();
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries<BaseAuditableEntity>();

            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    // entry.Property(x => x.CreatedDate).CurrentValue = DateTime.UtcNow;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Property(x => x.UpdatedDate).CurrentValue = DateTime.UtcNow;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
