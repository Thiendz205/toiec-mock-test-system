using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToeicMockTest.SharedKernel.Common.Enums;

namespace ToeicMockTest.Contracts.Questions.Requests
{
    public class UpdateQuestionRequest
    {
        public string Content { get; set; }
        public float Score { get; set; }
        public Guid DifficultyId { get; set; }
        public string? Explanation { get; set; } 
        public Guid? QuestionGroupId { get; set; }
        public RecordStatus Status { get; set; }
        public List<UpdateAnswerRequest> Answers { get; set; } = new List<UpdateAnswerRequest>();
        public List<Guid> TagIds { get; set; } = new List<Guid>();

    }
}
