using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PostPalBackend.Data;
using PostPalBackend.Models.Base;

namespace PostPalBackend.Repositories.GenericRepository
{
	public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
	{
		protected readonly PostPalDbContext Context;
		protected readonly DbSet<TEntity> Table;

		public GenericRepository(PostPalDbContext context)
		{
			this.Context = context;
			this.Table = context.Set<TEntity>();
		}

		public List<TEntity> GetAll()
		{
			return this.Table.ToList();
		}

		public async Task<List<TEntity>> GetAllAsync()
		{
			return await this.Table.AsNoTracking().ToListAsync();
		}
		public void Create(TEntity entity)
		{
			this.Table.Add(entity);
		}

		public async Task CreateAsync(TEntity entity)
		{
			await this.Table.AddAsync(entity);
		}

		public void CreateRange(IEnumerable<TEntity> entities)
		{
			this.Table.AddRange(entities);
		}

		public async Task CreateRangeAsync(IEnumerable<TEntity> entities)
		{
			await this.Table.AddRangeAsync(entities);
		}

		public void Update(TEntity entity)
		{
			this.Table.Update(entity);
		}

		public void UpdateRange(IEnumerable<TEntity> entities)
		{
			this.Table.UpdateRange(entities);
		}

		public void Delete(TEntity entity)
		{
			this.Table.Remove(entity);
		}

		public void DeleteRange(IEnumerable<TEntity> entities)
		{
			this.Table.RemoveRange(entities);
		}

		public TEntity? FindById(object id)
		{
			return this.Table.Find(id);
		}

		public async Task<TEntity?> FindByIdAsync(object id)
		{
			return await this.Table.FindAsync(id);
		}

		public bool Save()
		{
			try
			{
				return this.Context.SaveChanges() > 0;
			}
			catch (SqlException ex)
			{
				Console.WriteLine(ex);
			}

			return false;
		}

		public async Task<bool> SaveAsync()
		{
			try
			{
				return await this.Context.SaveChangesAsync() > 0;
			}
			catch (SqlException ex)
			{
				Console.WriteLine(ex);
			}

			return false;
		}
	}
}
