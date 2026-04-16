using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToeicMockTest.Domain.Entities.Abstractions;
namespace ToeicMockTest.Contracts.Questions.Requests
{
    public class UpdateAnswerRequest : IAnswerUpdateItem
    {
        public Guid? Id { get; set; }
        public string Content { get; set; } = null!;
        public bool IsCorrect { get; set; }
        public int Order { get; set; }
    }
}
