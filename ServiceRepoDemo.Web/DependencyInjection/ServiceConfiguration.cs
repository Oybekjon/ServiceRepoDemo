using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ServiceRepoDemo.Data;
using ServiceRepoDemo.Data.SqlServer;
using ServiceRepoDemo.DomainObjects;
using ServiceRepoDemo.Service;
using ServiceRepoDemo.Service.Implementation;
using System.Configuration;
namespace ServiceRepoDemo.Web.DependencyInjection
{
    public static class ServiceConfiguration
    {
        public static void RegisterServices(this IServiceCollection services, string connectionString) {
            services.AddDbContext<MainContext>(options => options.UseSqlServer(connectionString));
            services.AddScoped<DbContext, MainContext>();
            services.AddScoped<IRepository<User>, Repository<User>>();
            services.AddScoped<IRepository<Item>, Repository<Item>>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IItemService, ItemService>();
            
        }
    }
}
