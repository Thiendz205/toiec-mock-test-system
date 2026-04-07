using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToeicMockTest.Domain.Common;

namespace ToeicMockTest.Domain.Entities
{
    public class ExamCollection : BaseAuditableEntity
    {
        public string Name { get; private set; }
        public string? Description { get; private set; }
        public string? ImageUrl { get; private set; }

        public ICollection<Exam> Exams { get; private set; } = new List<Exam>();

        private ExamCollection() { }

        public ExamCollection(string name, Guid createdById, string? description = null, string? imageUrl = null)
        {
            Name = name;
            Description = description;
            ImageUrl = imageUrl;
            CreatedById = createdById; 
        }

        public void Update(string name, string? description, string? imageUrl, Guid updatedById)
        {
            Name = name;
            Description = description;
            ImageUrl = imageUrl;
            SetUpdatedInfo(updatedById);
        }
    }
}
