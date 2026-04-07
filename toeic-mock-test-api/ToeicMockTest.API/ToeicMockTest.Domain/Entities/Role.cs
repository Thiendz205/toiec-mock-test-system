using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToeicMockTest.Domain.Common;

namespace ToeicMockTest.Domain.Entities
{
    public class Role : BaseEntity
    {
        public string Name { get; private set; }
        public string Description { get; private set; }

        // Nếu IsSystem = true, Admin không được xóa hoặc đổi tên Role này để tránh lỗi code
        public bool IsSystem { get; private set; }

        public ICollection<User> Users { get; private set; } = new List<User>();

        private Role() { }

        // Constructor dùng cho việc tạo Role mới từ giao diện quản lý (mặc định IsSystem = false)
        public Role(string name, string description = "")
        {
            Name = name;
            Description = description;
            IsSystem = false;
        }

        // Phương thức tĩnh dùng cho Seed Data (gán ID và IsSystem cố định)
        public static Role CreateSystemRole(Guid id, string name, string description)
        {
            return new Role
            {
                Id = id,
                Name = name,
                Description = description,
                IsSystem = true
            };
        }

        public void Update(string name, string description)
        {
            if (IsSystem) throw new Exception("Không thể sửa Role hệ thống.");
            Name = name;
            Description = description;
        }
    }
}
