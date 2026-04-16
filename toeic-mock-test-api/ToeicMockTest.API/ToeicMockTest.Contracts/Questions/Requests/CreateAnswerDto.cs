using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToeicMockTest.Contracts.Questions.Requests
{
    public class CreateAnswerDto
    {
        public string Content { get; set; }
        public bool IsCorrect { get; set; }
        public int Order { get; set; }
    }
}
