using Microsoft.EntityFrameworkCore;
using PostPalBackend.Models;
using PostPalBackend.Models.Enums;

namespace PostPalBackend.Data
{
	public class PostPalDbContext : DbContext
	{
		public DbSet<User> Users { get; set; }

		public DbSet<UserProfile> Profiles { get; set; }

		public PostPalDbContext(DbContextOptions<PostPalDbContext> options) : base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<User>(user =>
			{
				user.ToTable(tb => tb.HasTrigger("Users_UPDATE"));
			});
			modelBuilder.Entity<User>()
				.Property(x => x.DateCreated)
				.HasDefaultValueSql("getdate()");
			modelBuilder.Entity<User>()
				.Property(x => x.Role)
				.HasDefaultValue(Role.User);

			modelBuilder.Entity<UserProfile>(profile =>
			{
				profile.ToTable(tb => tb.HasTrigger("Profiles_UPDATE"));
			});
			modelBuilder.Entity<UserProfile>()
				.Property(x => x.DateCreated)
				.HasDefaultValueSql("getdate()");
			modelBuilder.Entity<UserProfile>()
				.HasOne(e => e.User)
				.WithOne()
				.HasForeignKey<UserProfile>(e => e.UserId)
				.IsRequired();
		}
	}
}
