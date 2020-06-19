using ArnoAdminCore.Base.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ArnoAdminCore.Base.Repositories
{
    public class BaseRepository<TEntity> : BaseExpression<TEntity> where TEntity : class, IId
    {
        protected DbContext _context;
        public DbContext DbContext { get => this._context; }

        public BaseRepository(DbContext context)
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public IDbContextTransaction BeginTransaction()
        {
            return this._context.Database.BeginTransaction();
        }
        public IEnumerable<TEntity> FindAll()
        {
            var query = GetQuery();
            return query.OrderByDescending(x => x.Id).ToList();
        }
        private IQueryable<TEntity> GetQueryExpression(BaseSearch searcher, out int totalCount)
        {
            if (searcher == null)
            {
                throw new ArgumentNullException(nameof(searcher));
            }

            var query = GetQuery();
            var exp = CreateExpression(searcher);
            if (exp != null)
            {
                query = query.Where(exp);
            }

            var pageSearcher = searcher as BasePageSearch;

            if (pageSearcher != null)
            {
                totalCount = query.Count();
            }
            else
            {
                totalCount = 0;
            }

            if (!String.IsNullOrWhiteSpace(searcher.SortColumn))
            {
                query = searcher.SortType == "descending" ? OrderByDescending(query, searcher.SortColumn) : OrderBy(query, searcher.SortColumn);
            }

            if (pageSearcher != null)
            {
                int pageNum = pageSearcher.PageNum < 1 ? 1 : pageSearcher.PageNum;
                int pageSize = pageSearcher.PageSize;
                query = query.Skip((pageNum - 1) * pageSize).Take(pageSize);
            }

            return query;
        }
        public IEnumerable<TEntity> FindAll(BaseSearch baseSearch)
        {
            int totalCount;
            return GetQueryExpression(baseSearch, out totalCount).ToList();
        }
        public PageList<TEntity> FindAll(BasePageSearch pageSearcher)
        {
            int totalCount;
            var list = GetQueryExpression(pageSearcher, out totalCount).ToList();

            return new PageList<TEntity>(list, totalCount);
        }
        public TEntity FindById(long id)
        {
            return GetQuery().FirstOrDefault(x => x.Id == id);
        }
        public IEnumerable<TEntity> FindByIds(IEnumerable<long> ids)
        {
            return GetQuery().Where(x => ids.Contains(x.Id)).OrderByDescending(x => x.Id).ToList();
        }
        public TEntity Add(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            ICreate baseEntity = entity as ICreate;
            if (baseEntity != null)
            {
                baseEntity.Create();
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
            IUpdate baseEntity = entity as IUpdate;
            if (baseEntity != null)
            {
                baseEntity.Update();
            }
            _context.Set<TEntity>().Update(entity);
            return entity;
        }
        public TEntity UpdatePartial(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            IUpdate baseEntity = entity as IUpdate;
            if (baseEntity != null)
            {
                baseEntity.Update();
            }
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
        public void DeleteRange(params TEntity[] entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException(nameof(entities));
            }

            _context.Set<TEntity>().RemoveRange(entities);
        }
        public void DeleteRange(params long[] ids)
        {
            if (ids == null)
            {
                throw new ArgumentNullException(nameof(ids));
            }

            _context.Set<TEntity>().RemoveRange(_context.Set<TEntity>().Where(x => ids.Contains(x.Id)));
        }
        public bool Exists(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            return Exists(entity.Id);
        }
        public bool Exists(long id)
        {
            if (id == 0)
            {
                throw new ArgumentNullException(nameof(id));
            }

            return _context.Set<TEntity>().Any(x => x.Id == id);
        }
        public bool Save()
        {
            return _context.SaveChanges() > 0;
        }

        private IQueryable<TEntity> GetQuery()
        {
            return typeof(IDelete).IsAssignableFrom(typeof(TEntity)) ? _context.Set<TEntity>().Where(CreateDeleteExpression()) : _context.Set<TEntity>().AsQueryable();
        }
    }
}
