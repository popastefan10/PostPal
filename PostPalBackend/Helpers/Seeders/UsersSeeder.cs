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
					FirstName = "Admin",
					LastName = "Admin",
					Email = "adminPostPal@gmail.com",
					Username = "admin",
					Role = Role.Admin,
					PasswordHash = BCryptNet.HashPassword("admin1234")
				};

				postPalDbContext.Users.Add(SuperAdmin);
				postPalDbContext.SaveChanges();
			}
		}
	}
}
