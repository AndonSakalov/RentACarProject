using Microsoft.EntityFrameworkCore;
using RentACar.Data.Repository.Interfaces;

namespace RentACar.Tests.Repositories
{
	public class InMemoryRepository<T, TKey> : IRepository<T, TKey> where T : class
	{
		private readonly DbContext _context;

		public InMemoryRepository(DbContext context)
		{
			_context = context;
		}

		public void Add(T item)
		{
			_context.Set<T>().Add(item);
			_context.SaveChanges();
		}

		public async Task AddAsync(T item)
		{
			await _context.Set<T>().AddAsync(item);
			await _context.SaveChangesAsync();
		}

		public void AddRange(T[] items)
		{
			_context.Set<T>().AddRange(items);
			_context.SaveChanges();
		}

		public async Task AddRangeAsync(T[] items)
		{
			await _context.Set<T>().AddRangeAsync(items);
			await _context.SaveChangesAsync();
		}

		public bool Delete(TKey id)
		{
			var entity = GetById(id);
			if (entity == null) return false;

			_context.Set<T>().Remove(entity);
			_context.SaveChanges();
			return true;
		}

		public async Task<bool> DeleteAsync(TKey id)
		{
			var entity = await GetByIdAsync(id);
			if (entity == null) return false;

			_context.Set<T>().Remove(entity);
			await _context.SaveChangesAsync();
			return true;
		}

		public IEnumerable<T> GetAll()
		{
			return _context.Set<T>().ToList();
		}

		public async Task<IEnumerable<T>> GetAllAsync()
		{
			return await _context.Set<T>().ToListAsync();
		}

		public IQueryable<T> GetAllAttached()
		{
			return _context.Set<T>().AsQueryable();
		}

		public T GetById(TKey id)
		{
			return _context.Set<T>().Find(id);
		}

		public async Task<T> GetByIdAsync(TKey id)
		{
			return await _context.Set<T>().FindAsync(id);
		}

		public bool Update(T item)
		{
			try
			{
				_context.Set<T>().Update(item);
				_context.SaveChanges();
				return true;
			}
			catch
			{
				return false;
			}
		}

		public async Task<bool> UpdateAsync(T item)
		{
			try
			{
				_context.Set<T>().Update(item);
				await _context.SaveChangesAsync();
				return true;
			}
			catch
			{
				return false;
			}
		}
	}

}
