using ArnoAdminCore.Base.Models;
using ArnoAdminCore.Base.Repositories;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace ArnoAdminCore.Base.Services.Impl
{
    public class BaseCRUDService<TEntity> : IBaseCRUDService<TEntity> where TEntity: BaseEntity
    {
        protected readonly BaseRepository<TEntity> _repo;
        public BaseRepository<TEntity> Repository { get => _repo; }
        public BaseCRUDService(BaseRepository<TEntity> repo)
        {
            this._repo = repo;
        }
        public virtual IEnumerable<TEntity> FindAll()
        {
            return _repo.FindAll();
        }
        public virtual Task<IEnumerable<TEntity>> FindAllAsync()
        {
            return _repo.FindAllAsync();
        }
        public virtual PageList<TEntity> FindAll(BasePageSearch pageSearcg)
        {
            return _repo.FindAll(pageSearcg);
        }
        public virtual Task<PageList<TEntity>> FindAllAsync(BasePageSearch pageSearcg)
        {
            return _repo.FindAllAsync(pageSearcg);
        }
        public virtual TEntity FindById(long id)
        {
            return _repo.FindById(id);
        }
        public virtual Task<TEntity> FindByIdAsync(long id)
        {
            return _repo.FindByIdAsync(id);
        }
        public virtual IEnumerable<TEntity> FindByIds(IEnumerable<long> ids)
        {
            return _repo.FindByIds(ids);
        }
        public virtual Task<IEnumerable<TEntity>> FindByIdsAsync(IEnumerable<long> ids)
        {
            return _repo.FindByIdsAsync(ids);
        }
        public virtual TEntity Add(TEntity entity)
        {
            var e = _repo.Add(entity);
            _repo.Save();
            return e;
        }
        public virtual TEntity Update(TEntity entity)
        {
            var e = _repo.Update(entity);
            _repo.Save();
            return e;
        }
        public virtual void Delete(TEntity entity)
        {
            _repo.Delete(entity);
            _repo.Save();
        }
        public virtual void Delete(long id)
        {
            _repo.Delete(id);
            _repo.Save();
        }
        public virtual bool Exists(TEntity entity)
        {
            return _repo.Exists(entity);
        }
        public virtual Task<bool> ExistsAsync(TEntity entity)
        {
            return _repo.ExistsAsync(entity);
        }
        public virtual bool Exists(long id)
        {
            return _repo.Exists(id);
        }
        public virtual Task<bool> ExistsAsync(long id)
        {
            return _repo.ExistsAsync(id);
        }
        public virtual bool Save()
        {
            return _repo.Save();
        }
        public virtual Task<bool> SaveAsync()
        {
            return _repo.SaveAsync();
        }
    }
}
