using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToeicMockTest.Contracts.Questions.Requests
{
    public class CreateQuestionRequest
    {
        public string Content { get; set; }
        public float Score { get; set; }
        public Guid DifficultyId { get; set; }
        public Guid? QuestionGroupId { get; set; }
        public string? Explanation { get; set; }
        public List<CreateAnswerDto> Answers { get; set; } = new List<CreateAnswerDto>();
        public List<Guid> TagIds { get; set; } = new List<Guid>();
    }
}
