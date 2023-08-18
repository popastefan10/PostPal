using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using PostPalBackend.Models;
using PostPalBackend.Models.Enums;
using System.Text.Json;

namespace PostPalBackend.Data
{
	public class PostPalDbContext : DbContext
	{
		public DbSet<User> Users { get; set; }

		public DbSet<UserProfile> Profiles { get; set; }

		public DbSet<Post> Posts { get; set; }

		public DbSet<PostLike> PostLikes { get; set; }

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

			modelBuilder.Entity<Post>(post =>
			{
				post.ToTable(tb => tb.HasTrigger("Posts_UPDATE"));
			});
			modelBuilder.Entity<Post>()
				.Property(x => x.DateCreated)
				.HasDefaultValueSql("getdate()");
			modelBuilder.Entity<Post>()
				.HasOne(e => e.User)
				.WithMany()
				.HasForeignKey(e => e.UserId)
				.IsRequired();
			modelBuilder.Entity<Post>()
				.Property(x => x.ImagesUrls)
				.HasConversion(
					v => JsonSerializer.Serialize(v, new JsonSerializerOptions { WriteIndented = true }),
					v => JsonSerializer.Deserialize<List<string>>(v, null as JsonSerializerOptions)!,
					new ValueComparer<List<string>>(
						(c1, c2) => c1!.SequenceEqual(c2!),
						c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
						c => c.ToList()));
			modelBuilder.Entity<Post>()
				.HasMany<User>()
				.WithMany()
				.UsingEntity<PostLike>(
					l => l.HasOne<User>().WithMany().HasForeignKey(e => e.UserId).OnDelete(DeleteBehavior.Restrict),
					r => r.HasOne<Post>().WithMany(e => e.PostLikes).HasForeignKey(e => e.PostId).OnDelete(deleteBehavior: DeleteBehavior.Cascade));
		}
	}
}
