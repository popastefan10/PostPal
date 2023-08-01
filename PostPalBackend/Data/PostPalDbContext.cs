using Microsoft.EntityFrameworkCore;
using PostPalBackend.Models;
using PostPalBackend.Models.Enums;

namespace PostPalBackend.Data
{
	public class PostPalDbContext : DbContext
	{
		public DbSet<User> Users { get; set; }

		public PostPalDbContext(DbContextOptions<PostPalDbContext> options) : base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<User>()
				.Property(x => x.DateCreated)
				.HasDefaultValueSql("getdate()");
			modelBuilder.Entity<User>()
				.Property(x => x.Role)
				.HasDefaultValue(Role.User);
		}
	}
}
