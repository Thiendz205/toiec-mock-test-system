using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToeicMockTest.Domain.Common;

namespace ToeicMockTest.Domain.Entities
{
    public class Tag : BaseEntity
    {
        public string Name { get; private set; }
        public string? Category { get; private set; }

        public ICollection<QuestionTag> QuestionTags { get; private set; } = new List<QuestionTag>();

        private Tag() { }

        public Tag(string name, string? category = null)
        {
            Name = name;
            Category = category;
        }
    }
}
