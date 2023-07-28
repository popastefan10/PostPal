using PostPalBackend.Data;
using PostPalBackend.Models;
using PostPalBackend.Models.Enums;
using BCryptNet = BCrypt.Net.BCrypt;

namespace PostPalBackend.Helpers.Seeders
{
	public class UsersSeeder
	{
		public readonly PostPalDbContext postPalDbContext;

		public UsersSeeder(PostPalDbContext postPalDbContext)
		{
			this.postPalDbContext = postPalDbContext;
		}

		public void SeedInitialUsers()
		{
			if (!postPalDbContext.Users.Any())
			{
				var SuperAdmin = new User
				{
					Email = "admin@gmail.com",
					Role = Role.Admin,
					PasswordHash = BCryptNet.HashPassword("admin1234")
				};

				postPalDbContext.Users.Add(SuperAdmin);
				postPalDbContext.SaveChanges();
			}
		}
	}
}
