using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using ToeicMockTest.Domain.Repositories;
using ToeicMockTest.Domain.Repositories.Users;
using ToeicMockTest.Infrastructure.Persistence.Repositories.Users;
using ToeicMockTest.Infrastructure.Persistence;
using ToeicMockTest.Domain.Repositories.Roles;
using ToeicMockTest.Infrastructure.Persistence.Repositories.Roles;
using ToeicMockTest.Domain.Repositories.Questions;
using ToeicMockTest.Infrastructure.Persistence.Repositories.Questions;

namespace ToeicMockTest.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // Đăng ký DbContext
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IQuestionRepository, QuestionRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
