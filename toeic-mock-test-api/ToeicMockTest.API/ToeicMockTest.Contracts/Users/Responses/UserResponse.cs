using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToeicMockTest.SharedKernel.Common.Enums;

namespace ToeicMockTest.Contracts.Users.Responses
{
    public class UserResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string FullName => $"{FirstName} {LastName}";
        public Guid RoleId { get; set; }
        public string RoleName { get; set; } = string.Empty; 
        public RecordStatus Status { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
