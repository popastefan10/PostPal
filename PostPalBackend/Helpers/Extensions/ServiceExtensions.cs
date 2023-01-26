using PostPalBackend.Helpers.Seeders;
using PostPalBackend.Repositories.UserRepository;

namespace PostPalBackend.Helpers.Extensions {
	public static class ServiceExtensions {
		public static IServiceCollection AddRepositories(this IServiceCollection services) {
			services.AddTransient<IUserRepository, UserRepository>();

			return services;
		}

		public static IServiceCollection AddSeeders(this IServiceCollection services) {
			services.AddTransient<UsersSeeder>();

			return services;
		}
	}
}
