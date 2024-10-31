using Microsoft.EntityFrameworkCore;
using RentACar.Data.Repository.Interfaces;

namespace RentACar.Data.Repository
{
    public class Repository<TType, TId> : IRepository<TType, TId>
        where TType : class
    {
        private readonly RentACarDbContext context;
        private readonly DbSet<TType> dbSet;
        public Repository(RentACarDbContext context)
        {
            this.context = context;
            this.dbSet = this.context.Set<TType>();
        }
        public void Add(TType item)
        {
            this.dbSet.Add(item);
            this.context.SaveChanges();
        }
        public async Task AddAsync(TType item)
        {
            await this.dbSet.AddAsync(item);
            await this.context.SaveChangesAsync();
        }
        public void AddRange(TType[] items)
        {
            this.dbSet.AddRange(items);
            this.context.SaveChanges();
        }
        public async Task AddRangeAsync(TType[] items)
        {
            await this.dbSet.AddRangeAsync(items);
            await this.context.SaveChangesAsync();
        }
        public bool Delete(TId id)
        {
            TType entityToDelete = this.GetById(id);
            if (entityToDelete == null)
            {
                return false;
            }

            this.dbSet.Remove(entityToDelete);
            this.context.SaveChanges();

            return true;
        }
        public async Task<bool> DeleteAsync(TId id)
        {
            TType entityToDelete = await this.GetByIdAsync(id);
            if (entityToDelete == null)
            {
                return false;
            }

            this.dbSet.Remove(entityToDelete);
            await this.context.SaveChangesAsync();

            return true;
        }
        public IEnumerable<TType> GetAll()
        {
            return this.dbSet.ToArray();
        }
        public async Task<IEnumerable<TType>> GetAllAsync()
        {
            return await this.dbSet.ToArrayAsync();
        }
        public IQueryable<TType> GetAllAttached()
        {
            return this.dbSet.AsQueryable();
        }
        public TType GetById(TId id)
        {
            TType entity = this.dbSet.Find(id);

            return entity;
        }
        public async Task<TType> GetByIdAsync(TId id)
        {
            TType entity = await this.dbSet.FindAsync(id);

            return entity;
        }
        public bool Update(TType item)
        {
            try
            {
                this.dbSet.Attach(item);
                this.context.Entry(item).State = EntityState.Modified;
                this.context.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        public async Task<bool> UpdateAsync(TType item)
        {
            try
            {
                this.dbSet.Attach(item);
                this.context.Entry(item).State = EntityState.Modified;
                await this.context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
