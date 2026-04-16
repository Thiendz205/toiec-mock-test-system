using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToeicMockTest.Domain.Common;
using ToeicMockTest.Domain.Entities.Abstractions;
using ToeicMockTest.SharedKernel.Common.Enums;

namespace ToeicMockTest.Domain.Entities
{
    public class Question : BaseAuditableEntity
    {
        public string Content { get; private set; }
        public float Score { get; private set; }
        public string? Explanation { get; private set; }

        // Difficulty
        public Guid DifficultyId { get; private set; }
        public Difficulty Difficulty { get; private set; } = null!;

        public Guid? QuestionGroupId { get; private set; }
        public QuestionGroup? QuestionGroup { get; private set; }

        // Navigation
        public User CreatedBy { get; private set; } = null!;
        public ICollection<Answer> Answers { get; private set; } = new List<Answer>();
        public ICollection<QuestionTag> QuestionTags { get; private set; } = new List<QuestionTag>();
        public ICollection<QuestionUpdateHistory> UpdateHistories { get; private set; } = new List<QuestionUpdateHistory>();

        private Question() { }

        public Question(
            string content,
            float score,
            Guid difficultyId,
            Guid createdById,
            Guid? questionGroupId = null,
            string? explanation = null)
        {
            Content = content;
            Score = score;
            DifficultyId = difficultyId;
            CreatedById = createdById;
            QuestionGroupId = questionGroupId;
            Explanation = explanation;
        }

        public void AddAnswer(string content, bool isCorrect, int order)
        {
            if (isCorrect && Answers.Any(a => a.IsCorrect))
                throw new InvalidOperationException("Only one correct answer is allowed.");

            Answers.Add(new Answer(content, isCorrect, order, Id));
        }

        public void AddTag(Guid tagId)
        {
            if (!QuestionTags.Any(t => t.TagId == tagId))
            {
                QuestionTags.Add(new QuestionTag(Id, tagId));
            }
        }

        public void Update(string content, float score, Guid difficultyId, string? explanation, Guid? questionGroupId, Guid updatedById)
        {
            Content = content;
            Score = score;
            DifficultyId = difficultyId;
            Explanation = explanation;
            QuestionGroupId = questionGroupId;
            SetUpdatedInfo(updatedById);
        }
        // Trong file Question.cs
        public void UpdateTags(IEnumerable<Guid> newTagIds)
        {
            if (newTagIds == null) return;

            var tagsToRemove = QuestionTags
                .Where(qt => !newTagIds.Contains(qt.TagId))
                .ToList();

            foreach (var tag in tagsToRemove)
            {
                QuestionTags.Remove(tag);
            }

            var currentTagIds = QuestionTags.Select(qt => qt.TagId).ToHashSet();
            foreach (var tagId in newTagIds)
            {
                if (!currentTagIds.Contains(tagId))
                {
                    QuestionTags.Add(new QuestionTag(this.Id, tagId));
                }
            }
        }
        public void UpdateAnswers(IEnumerable<IAnswerUpdateItem> newAnswers)
        {
            if (newAnswers == null || !newAnswers.Any())
                throw new InvalidOperationException("Question must have at least one answer.");

            if (newAnswers.Count(a => a.IsCorrect) != 1)
                throw new InvalidOperationException("A question must have exactly one correct answer.");

            var requestIds = newAnswers.Where(a => a.Id.HasValue).Select(a => a.Id!.Value).ToHashSet();
            var toRemove = Answers.Where(a => !requestIds.Contains(a.Id)).ToList();
            foreach (var item in toRemove) Answers.Remove(item);

            foreach (var req in newAnswers)
            {
                if (req.Id.HasValue)
                {
                    var existing = Answers.FirstOrDefault(a => a.Id == req.Id.Value);
                    existing?.Update(req.Content, req.IsCorrect, req.Order);
                }
                else
                {
                    AddAnswer(req.Content, req.IsCorrect, req.Order);
                }
            }
        }
    }
}
