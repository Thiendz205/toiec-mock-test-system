using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToeicMockTest.Domain.Common;

namespace ToeicMockTest.Domain.Entities
{
    public class QuestionTag : BaseEntity
    {
        public Guid QuestionId { get; private set; }
        public Question Question { get; private set; } = null!;

        public Guid TagId { get; private set; }
        public Tag Tag { get; private set; } = null!;

        private QuestionTag() { }

        public QuestionTag(Guid questionId, Guid tagId)
        {
            QuestionId = questionId;
            TagId = tagId;
        }
    }
}
