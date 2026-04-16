using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using ToeicMockTest.Application.Services.Questions;
using ToeicMockTest.Application.Services.Roles;
using ToeicMockTest.Application.Services.Users;
using ToeicMockTest.Domain.Repositories.Roles;
namespace ToeicMockTest.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IQuestionService, QuestionService>();
            return services;
        }
    }
}
