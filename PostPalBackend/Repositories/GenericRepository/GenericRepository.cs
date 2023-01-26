using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PostPalBackend.Data;
using PostPalBackend.Models.Base;

namespace PostPalBackend.Repositories.GenericRepository {
	public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity {
		protected readonly PostPalDbContext context;
		protected readonly DbSet<TEntity> table;

		public GenericRepository(PostPalDbContext context) {
			this.context = context;
			this.table = context.Set<TEntity>();
		}

		public List<TEntity> GetAll() {
			return this.table.ToList();
		}

		public async Task<List<TEntity>> GetAllAsync() {
			return await this.table.AsNoTracking().ToListAsync();
		}
		public void Create(TEntity entity) {
			this.table.Add(entity);
		}

		public async Task CreateAsync(TEntity entity) {
			await this.table.AddAsync(entity);
		}

		public void CreateRange(IEnumerable<TEntity> entities) {
			this.table.AddRange(entities);
		}

		public async Task CreateRangeAsync(IEnumerable<TEntity> entities) {
			await this.table.AddRangeAsync(entities);
		}

		public void Update(TEntity entity) {
			this.table.Update(entity);
		}

		public void UpdateRange(IEnumerable<TEntity> entities) {
			this.table.UpdateRange(entities);
		}

		public void Delete(TEntity entity) {
			this.table.Remove(entity);
		}

		public void DeleteRange(IEnumerable<TEntity> entities) {
			this.table.RemoveRange(entities);
		}

		public TEntity? FindById(object id) {
			return this.table.Find(id);
		}

		public async Task<TEntity?> FindByIdAsync(object id) {
			return await this.table.FindAsync(id);
		}

		public bool Save() {
			try {
				return this.context.SaveChanges() > 0;
			}
			catch (SqlException ex) {
				Console.WriteLine(ex);
			}

			return false;
		}

		public async Task<bool> SaveAsync() {
			try {
				return await this.context.SaveChangesAsync() > 0;
			}
			catch (SqlException ex) {
				Console.WriteLine(ex);
			}

			return false;
		}
	}
}
