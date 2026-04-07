using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToeicMockTest.Domain.Common;
using ToeicMockTest.Domain.Common.Enums;

namespace ToeicMockTest.Domain.Entities
{
    public class QuestionGroup : BaseAuditableEntity
    {
        public string? Content { get; private set; }
        public string? AudioUrl { get; private set; }
        public string? ImageUrl { get; private set; }
        public string? Transcript { get; private set; }
        public ToeicPart Part { get; private set; }

        public User CreatedBy { get; private set; } = null!;
        public ICollection<Question> Questions { get; private set; } = new List<Question>();

        private QuestionGroup() { }

        public QuestionGroup(
            ToeicPart part,
            Guid createdById,
            string? content = null,
            string? audioUrl = null,
            string? imageUrl = null,
            string? transcript = null)
        {
            Part = part;
            CreatedById = createdById;
            Content = content;
            AudioUrl = audioUrl;
            ImageUrl = imageUrl;
            Transcript = transcript;
        }

        public void Update(string? content, string? audioUrl, string? imageUrl, string? transcript, Guid updatedById)
        {
            Content = content;
            AudioUrl = audioUrl;
            ImageUrl = imageUrl;
            Transcript = transcript;
            SetUpdatedInfo(updatedById);
        }
    }
}
