using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToeicMockTest.Domain.Common;
using ToeicMockTest.SharedKernel.Common.Enums;
namespace ToeicMockTest.Domain.Entities
{
    public class Difficulty : BaseAuditableEntity
    {
        public string Name { get; private set; }
        public int LevelOrder { get; private set; }
        public string? Description { get; private set; }

        public User? CreatedBy { get; private set; }
        public User? UpdatedBy { get; private set; }

        private Difficulty() { }

        public Difficulty(string name, int levelOrder, string? description, Guid? createdById)
        {
            Name = name;
            LevelOrder = levelOrder;
            Description = description;
            CreatedById = createdById;
            Status = RecordStatus.Active;
            CreatedDate = DateTime.UtcNow;
        }

        public void Update(string name, int levelOrder, string? description, Guid? updatedById)
        {
            Name = name;
            LevelOrder = levelOrder;
            Description = description;
            if (updatedById.HasValue)
                SetUpdatedInfo(updatedById.Value);
        }
    }
}
