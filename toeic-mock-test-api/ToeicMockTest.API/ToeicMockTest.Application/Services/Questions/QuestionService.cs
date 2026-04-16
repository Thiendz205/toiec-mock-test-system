using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToeicMockTest.Contracts.Questions.Requests;
using ToeicMockTest.Contracts.Questions.Responses;
using ToeicMockTest.Domain.Entities;
using ToeicMockTest.Domain.Repositories;
using ToeicMockTest.Domain.Repositories.Questions;

namespace ToeicMockTest.Application.Services.Questions
{
    public class QuestionService : IQuestionService
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly IUnitOfWork _unitOfWork;

        public QuestionService(IQuestionRepository questionRepository, IUnitOfWork unitOfWork)
        {
            _questionRepository = questionRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<QuestionResponse?> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            var question = await _questionRepository.GetWithDetailsAsync(id, ct);
            if (question == null) return null;

            return MapToResponse(question);
        }

        public async Task<IEnumerable<QuestionResponse>> GetAllAsync(CancellationToken ct = default)
        {
            var questions = await _questionRepository.GetAllAsync(ct);
            return questions.Select(MapToResponse);
        }

        public async Task<Guid> CreateAsync(CreateQuestionRequest request, Guid currentUserId, CancellationToken ct = default)
        {
            // 1. Khởi tạo Entity
            var question = new Question(
                request.Content,
                request.Score,
                request.DifficultyId,
                currentUserId,
                request.QuestionGroupId,
                request.Explanation
            );

            // 2. Thêm Answers
            foreach (var ans in request.Answers)
            {
                question.AddAnswer(ans.Content, ans.IsCorrect, ans.Order);
            }

            // 3. Thêm Tags
            foreach (var tagId in request.TagIds)
            {
                question.AddTag(tagId);
            }

            await _questionRepository.AddAsync(question, ct);
            await _unitOfWork.SaveChangesAsync(ct);

            return question.Id;
        }

        public async Task UpdateAsync(Guid id, UpdateQuestionRequest request, Guid currentUserId, CancellationToken ct = default)
        {
            var question = await _questionRepository.GetWithDetailsAsync(id, ct);
            if (question == null) throw new Exception("Question not found");

            // 1. Cập nhật thông tin cơ bản
            question.Update(
                request.Content,
                request.Score,
                request.DifficultyId,
                request.Explanation,
                request.QuestionGroupId,
                currentUserId
            );

            // 2. Cập nhật Tags
            if (request.TagIds != null)
            {
                question.UpdateTags(request.TagIds);
            }

            // 3. CẬP NHẬT ANSWERS
            if (request.Answers != null && request.Answers.Any())
            {
                question.UpdateAnswers(request.Answers);
            }

            // 4. Xử lý trạng thái
            if (request.Status == SharedKernel.Common.Enums.RecordStatus.Delete)
                question.SoftDelete();
            else
                question.Activate();

            // 5. Lưu thay đổi
            _questionRepository.Update(question);
            await _unitOfWork.SaveChangesAsync(ct);
        }

        public async Task DeleteAsync(Guid id, CancellationToken ct = default)
        {
            var question = await _questionRepository.GetByIdAsync(id, ct);
            if (question != null)
            {
                // Tùy vào yêu cầu, bạn có thể gọi SoftDelete() thay vì Delete cứng
                _questionRepository.Delete(question);
                await _unitOfWork.SaveChangesAsync(ct);
            }
        }

        private static QuestionResponse MapToResponse(Question q)
        {
            return new QuestionResponse
            {
                Id = q.Id,
                Content = q.Content,
                Score = q.Score,
                Explanation = q.Explanation,
                DifficultyId = q.DifficultyId,
                DifficultyName = q.Difficulty?.Name ?? "N/A",
                QuestionGroupId = q.QuestionGroupId,
                Status = q.Status,
                CreatedDate = q.CreatedDate,
                CreatedById = q.CreatedById,
                Answers = q.Answers.OrderBy(a => a.Order).Select(a => new AnswerResponse
                {
                    Id = a.Id,
                    Content = a.Content,
                    IsCorrect = a.IsCorrect,
                    Order = a.Order
                }).ToList(),
                Tags = q.QuestionTags.Select(qt => new QuestionTagResponse
                {
                    TagId = qt.TagId,
                    TagName = qt.Tag?.Name ?? ""
                }).ToList()
            };
        }
    }
}
