using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToeicMockTest.SharedKernel.Common.Enums;

namespace ToeicMockTest.Contracts.Questions.Responses
{
    public class QuestionResponse
    {
        public Guid Id { get; set; }
        public string Content { get; set; } = null!;
        public float Score { get; set; }
        public string? Explanation { get; set; } 
        public Guid DifficultyId { get; set; }
        public string DifficultyName { get; set; } = null!;
        public Guid? QuestionGroupId { get; set; }
        public RecordStatus Status { get; set; } 
        public List<AnswerResponse> Answers { get; set; } = new List<AnswerResponse>();
        public List<QuestionTagResponse> Tags { get; set; } = new List<QuestionTagResponse>();
        public DateTime CreatedDate { get; set; } 
        public Guid? CreatedById { get; set; }
    }
}
