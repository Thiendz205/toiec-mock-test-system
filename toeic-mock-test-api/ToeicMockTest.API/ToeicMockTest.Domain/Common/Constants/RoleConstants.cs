using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToeicMockTest.Domain.Common.Constants
{
    public static class RoleConstants
    {
        // Tên Role để dùng trong code (Authorize, Logic check)
        public const string Admin = "Admin";
        public const string Staff = "Staff";
        public const string User = "User";

        // ID cố định để dùng cho Seed Data (đảm bảo môi trường nào ID cũng giống nhau)
        public static readonly Guid AdminId = Guid.Parse("A1B2C3D4-E5F6-4A7B-8C9D-0E1F2A3B4C5D");
        public static readonly Guid StaffId = Guid.Parse("B2C3D4E5-F6A7-4B8C-9D0E-1F2A3B4C5D6E");
        public static readonly Guid UserId = Guid.Parse("C3D4E5F6-A7B8-4C9D-0E1F-2A3B4C5D6E7F");
    }
}
