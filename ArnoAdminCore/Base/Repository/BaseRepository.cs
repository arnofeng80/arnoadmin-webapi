using ArnoAdminCore.Base.Models;
using ArnoAdminCore.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ArnoAdminCore.Base.Repository
{
    public class BaseRepository<TEntity> where TEntity : BaseEntity
    {
        protected DbContext _context;

        public BaseRepository(DbContext context)
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<IEnumerable<TEntity>> FindAllAsync()
        {
            return await _context.Set<TEntity>().Where(x => x.Deleted == 0).OrderByDescending(x => x.Id).ToListAsync();
        }
        public async Task<PageList<TEntity>> FindAllAsync(BasePageSearch pageSearcg)
        {
            if (pageSearcg == null)
            {
                throw new ArgumentNullException(nameof(pageSearcg));
            }
            var query = _context.Set<TEntity>().AsQueryable();

            int pageNum = pageSearcg.PageNum < 1 ? 1 : pageSearcg.PageNum;
            int pageSize = pageSearcg.PageSize;

            query = query.Skip((pageNum - 1) * pageSize).Take(pageSize);

            var list = query.ToList();
            var totalCount = await query.CountAsync();

            return new PageList<TEntity>(list, totalCount);
        }
        public TEntity FindById(long id)
        {
            return _context.Set<TEntity>().Where(x => x.Id == id && x.Deleted == 0).FirstOrDefault();
        }
        public async Task<TEntity> FindByIdAsync(long id)
        {
            return await _context.Set<TEntity>().Where(x => x.Id == id && x.Deleted == 0).FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<TEntity>> FindByIdsAsync(IEnumerable<long> ids)
        {
            return await _context.Set<TEntity>().OrderByDescending(x => x.Id).ToListAsync();
        }
        public TEntity Add(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            _context.Set<TEntity>().Add(entity);
            return entity;
        }
        public TEntity Update(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            _context.Set<TEntity>().Update(entity);
            return entity;
        }
        public void Delete(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            _context.Set<TEntity>().Remove(entity);
        }
        public void Delete(long id)
        {
            var entity = FindById(id);
            Delete(entity);
        }
        public async Task<bool> ExistsAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            return await ExistsAsync(entity.Id);
        }
        public async Task<bool> ExistsAsync(long id)
        {
            if (id == 0)
            {
                throw new ArgumentNullException(nameof(id));
            }

            return await _context.Set<TEntity>().AnyAsync(x => x.Id == id);
        }
        public async Task<bool> SaveAsync()
        {
            return await _context.SaveChangesAsync() >= 0;
        }
    }
}
