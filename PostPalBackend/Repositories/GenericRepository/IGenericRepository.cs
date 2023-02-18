using PostPalBackend.Models.Base;

namespace PostPalBackend.Repositories.GenericRepository
{
	public interface IGenericRepository<TEntity> where TEntity : BaseEntity
	{
		// Get
		List<TEntity> GetAll();
		Task<List<TEntity>> GetAllAsync();

		// Create
		void Create(TEntity entity);
		Task CreateAsync(TEntity entity);
		void CreateRange(IEnumerable<TEntity> entities);
		Task CreateRangeAsync(IEnumerable<TEntity> entities);

		// Update
		void Update(TEntity entity);
		void UpdateRange(IEnumerable<TEntity> entities);

		// Delete
		void Delete(TEntity entity);
		void DeleteRange(IEnumerable<TEntity> entities);

		// Find
		TEntity? FindById(Object id);
		Task<TEntity?> FindByIdAsync(Object id);

		// Save
		bool Save();
		Task<bool> SaveAsync();
	}
}
