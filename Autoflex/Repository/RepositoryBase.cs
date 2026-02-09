using Autoflex.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Autoflex.Repository
{
	public class RepositoryBase<T> : IRepositoryBase<T> where T : class
	{
		protected readonly AppDbContext _context;
		protected readonly DbSet<T> _dbSet;

		public RepositoryBase(AppDbContext context)
		{
			_context = context;
			_dbSet = context.Set<T>();
		}

		public async Task<IEnumerable<T>> GetAllAsync()
			=> await _dbSet.AsNoTracking().ToListAsync();

		public async Task<T?> GetByIdAsync(int id)
			=> await _dbSet.FindAsync(id);

		public async Task AddAsync(T entity)
		{
			_dbSet.Add(entity);
			await _context.SaveChangesAsync();
		}

		public async Task UpdateAsync(T entity)
		{
			_dbSet.Update(entity);
			await _context.SaveChangesAsync();
		}

		public async Task DeleteAsync(T entity)
		{
			_dbSet.Remove(entity);
			await _context.SaveChangesAsync();
		}
	}
}
