using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using ToeicMockTest.Domain.Common;
using ToeicMockTest.Domain.Common.Enums;

namespace ToeicMockTest.Domain.Entities
{
    public class UserVerificationToken : BaseAuditableEntity
    {
        public Guid UserId { get; private set; }
        public User User { get; private set; } = null!;

        public string TokenValue { get; private set; }
        public TokenType Type { get; private set; } 

        public DateTime ExpiryDate { get; private set; } 
        public bool IsUsed { get; private set; } 
        private UserVerificationToken() { }

        public UserVerificationToken(Guid userId, string tokenValue, TokenType type, int expireInMinutes = 5)
        {
            UserId = userId;
            TokenValue = tokenValue;
            Type = type;
            ExpiryDate = DateTime.UtcNow.AddMinutes(expireInMinutes);
            IsUsed = false;
        }

        public void MarkAsUsed()
        {
            IsUsed = true;
            Deactivate(); 
        }

        public bool IsValid() => !IsUsed && Status == RecordStatus.Active && DateTime.UtcNow <= ExpiryDate;
    }
}

