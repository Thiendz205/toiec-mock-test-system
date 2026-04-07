using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToeicMockTest.Domain.Common.Enums;
using ToeicMockTest.Domain.Entities;

namespace ToeicMockTest.Domain.Common
{
    public abstract class BaseAuditableEntity : BaseEntity
    {
        public RecordStatus Status { get; protected set; } = RecordStatus.Active;

        public DateTime CreatedDate { get; protected set; } = DateTime.UtcNow;
        public Guid? CreatedById { get; protected set; }

        public DateTime? UpdatedDate { get; protected set; }
        public Guid? UpdatedById { get; protected set; }

        public void Activate() => Status = RecordStatus.Active;
        public void Deactivate() => Status = RecordStatus.Inactive;

        // Helper để cập nhật thông tin audit nhanh
        public void SetUpdatedInfo(Guid updatedById)
        {
            UpdatedDate = DateTime.UtcNow;
            UpdatedById = updatedById;
        }
    }
}
