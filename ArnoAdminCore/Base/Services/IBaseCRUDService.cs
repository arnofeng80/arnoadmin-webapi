﻿using ArnoAdminCore.Base.Models;
using ArnoAdminCore.Base.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ArnoAdminCore.Base.Services
{
    public interface IBaseCRUDService<TEntity> where TEntity : BaseEntity
    {
        public BaseRepository<TEntity> Repository { get; }
        public IEnumerable<TEntity> FindAll();
        public Task<IEnumerable<TEntity>> FindAllAsync();
        public IEnumerable<TEntity> FindAll(BaseSearch pageSearch);
        public Task<IEnumerable<TEntity>> FindAllAsync(BaseSearch searcher);
        public PageList<TEntity> FindPage(BasePageSearch pageSearch);
        public TEntity FindById(long id);
        public IEnumerable<TEntity> FindByIds(IEnumerable<long> ids);
        public TEntity Add(TEntity entity);
        public TEntity Update(TEntity entity);
        public TEntity UpdatePartial(TEntity entity);
        public void Delete(TEntity entity);
        public void Delete(long id);
        public void DeleteRange(params TEntity[] entity);
        public void DeleteRange(params long[] id);
        public bool Exists(TEntity entity);
        public bool Exists(long id);
        public bool Save();
    }
}
