using PostPalBackend.Helpers.Seeders;

namespace PostPalBackend.Helpers.Extensions {
	public static class ServiceExtensions {
		public static IServiceCollection AddSeeders(this IServiceCollection services) {
			services.AddTransient<UsersSeeder>();
			return services;
		}
	}
}
