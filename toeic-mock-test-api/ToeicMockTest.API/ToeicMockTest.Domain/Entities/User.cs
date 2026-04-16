using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToeicMockTest.Domain.Common;
using ToeicMockTest.SharedKernel.Common.Enums;
namespace ToeicMockTest.Domain.Entities
{
    public class User : BaseAuditableEntity
    {
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string PasswordHash { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }

        public Guid RoleId { get; private set; }
        public Role Role { get; private set; } = null!;

        public ICollection<QuestionUpdateHistory> QuestionUpdateHistories { get; private set; }
            = new List<QuestionUpdateHistory>();

        private User() { }

        public User(string name, string email, string passwordHash, string firstName, string lastName, Guid roleId)
        {
            // Id, Status, CreatedDate tự khởi tạo ở lớp cha
            Name = name;
            Email = email;
            PasswordHash = passwordHash;
            FirstName = firstName;
            LastName = lastName;
            RoleId = roleId;
        }

        public void ChangeRole(Guid newRoleId)
        {
            RoleId = newRoleId;
        }

        public void UpdateProfile(string firstName, string lastName, Guid updatedById)
        {
            FirstName = firstName;
            LastName = lastName;
            SetUpdatedInfo(updatedById);
        }
        public void AdminUpdateUser(string firstName, string lastName, string email, Guid roleId, Guid updatedById)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email; 
            RoleId = roleId;
            SetUpdatedInfo(updatedById);
        }
        public void Restore()
        {
            if (Status == RecordStatus.Delete)
            {
                Activate();
            }
        }
    }
}
