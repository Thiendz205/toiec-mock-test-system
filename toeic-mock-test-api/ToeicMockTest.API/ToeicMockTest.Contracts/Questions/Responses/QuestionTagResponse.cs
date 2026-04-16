using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToeicMockTest.Contracts.Questions.Responses
{
    public class QuestionTagResponse
    {
        public Guid TagId { get; set; }
        public string TagName { get; set; }
    }
}
