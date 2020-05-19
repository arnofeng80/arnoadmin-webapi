using ArnoAdminCore.Base.Models;
using ArnoAdminCore.Context;
using ArnoAdminCore.SystemManage.Models.Dto.Search;
using ArnoAdminCore.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ArnoAdminCore.Base.Repositories
{
    public class BaseRepository<TEntity> where TEntity : BaseEntity
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
            return _context.Set<TEntity>().Where(x => x.Deleted == 0).OrderByDescending(x => x.Id).ToList();
        }
        public PageList<TEntity> FindAll(BasePageSearch pageSearch)
        {
            if (pageSearch == null)
            {
                throw new ArgumentNullException(nameof(pageSearch));
            }
            var exp = ExpressionHelper<TEntity>.CreateExpression(pageSearch);

            var query = exp == null ? _context.Set<TEntity>().AsQueryable() : _context.Set<TEntity>().Where(exp);

            int pageNum = pageSearch.PageNum < 1 ? 1 : pageSearch.PageNum;
            int pageSize = pageSearch.PageSize;

            var totalCount = query.Count();

            if (!String.IsNullOrWhiteSpace(pageSearch.SortColumn))
            {
                query = pageSearch.SortType == "descending" ? ExpressionHelper<TEntity>.OrderByDescending(query, pageSearch.SortColumn) : ExpressionHelper<TEntity>.OrderBy(query, pageSearch.SortColumn);
            }

            query = query.Skip((pageNum - 1) * pageSize).Take(pageSize);

            var list = query.ToList();

            return new PageList<TEntity>(list, totalCount);
        }
        public TEntity FindById(long id)
        {
            return _context.Set<TEntity>().Where(x => x.Id == id && x.Deleted == 0).FirstOrDefault();
        }
        public IEnumerable<TEntity> FindByIds(IEnumerable<long> ids)
        {
            return _context.Set<TEntity>().OrderByDescending(x => x.Id).ToList();
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
            return _context.SaveChanges() >= 0;
        }
    }
}
