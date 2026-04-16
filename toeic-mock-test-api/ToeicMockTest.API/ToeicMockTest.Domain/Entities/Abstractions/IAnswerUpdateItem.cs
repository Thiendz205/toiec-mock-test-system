using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToeicMockTest.Domain.Entities.Abstractions
{
    public interface IAnswerUpdateItem
    {
        Guid? Id { get; }
        string Content { get; }
        bool IsCorrect { get; }
        int Order { get; }
    }
}
