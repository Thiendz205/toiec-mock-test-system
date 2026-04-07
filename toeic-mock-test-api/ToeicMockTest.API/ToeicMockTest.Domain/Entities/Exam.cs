using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToeicMockTest.Domain.Common;
using ToeicMockTest.Domain.Common.Enums;

namespace ToeicMockTest.Domain.Entities
{
    public class Exam : BaseAuditableEntity
    {
        public string Title { get; private set; }
        public int DurationMinutes { get; private set; }

        private readonly List<ExamQuestion> _examQuestions = new();
        public IReadOnlyCollection<ExamQuestion> ExamQuestions => _examQuestions;

        public Guid? ExamCollectionId { get; private set; }
        public ExamCollection? ExamCollection { get; private set; }

        private Exam() { }

        public Exam(string title, int durationMinutes, Guid createdById, Guid? examCollectionId = null)
        {
            Title = title;
            DurationMinutes = durationMinutes;
            CreatedById = createdById;
            ExamCollectionId = examCollectionId;
        }
    }
}
