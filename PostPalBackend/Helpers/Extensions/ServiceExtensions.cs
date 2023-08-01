using AutoMapper;
using PostPalBackend.Helpers.Jwt;
using PostPalBackend.Helpers.Mappers;
using PostPalBackend.Helpers.Seeders;
using PostPalBackend.Repositories.UserRepository;
using PostPalBackend.Services.UserService;

namespace PostPalBackend.Helpers.Extensions
{
	public static class ServiceExtensions
	{
		public static IServiceCollection AddRepositories(this IServiceCollection services)
		{
			services.AddTransient<IUserRepository, UserRepository>();

			return services;
		}

		public static IServiceCollection AddSeeders(this IServiceCollection services)
		{
			services.AddTransient<UsersSeeder>();

			return services;
		}

		public static IServiceCollection AddServices(this IServiceCollection services)
		{
			services.AddTransient<IUserService, UserService>();

			var mapperConfig = new MapperConfiguration(mc =>
			{
				mc.AddProfile(new MapperProfile());
			});
			IMapper mapper = mapperConfig.CreateMapper();
			services.AddSingleton(mapper);

			return services;
		}

		public static IServiceCollection AddUtils(this IServiceCollection services)
		{
			services.AddScoped<IJwtUtils, JwtUtils>();

			return services;
		}
	}
}
