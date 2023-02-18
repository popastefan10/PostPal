using Microsoft.EntityFrameworkCore;
using PostPalBackend.Models;

namespace PostPalBackend.Data
{
	public class PostPalDbContext : DbContext
	{
		public DbSet<User> Users { get; set; }

		public PostPalDbContext(DbContextOptions<PostPalDbContext> options) : base(options)
		{
			System.Console.WriteLine("Context options: {0}", options);
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
		}
	}
}
