using Corex.Data.Infrastructure;
using Corex.ExceptionHandling.Derived.Data;
using Corex.ExceptionHandling.Infrastructure.Models;
using Corex.Model.Derived.EntityModel;
using Corex.Model.Infrastructure;
using Corex.Operation.Infrastructure;
using Corex.Validation.Infrastucture;
using PagedList.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Corex.Operation.Derived.DataOperation
{
    public abstract class BaseDataOperation<TEntity, TRepository, TKey> : IDataOperation<TEntity, TKey>
       where TEntity : class, IEntityModel<TKey>, new()
       where TRepository : class, IRepository<TEntity, TKey>
    {
        public IRepository<TEntity, TKey> Repository { get; protected set; }
        public abstract IRepository<TEntity, TKey> SetRepository();

        public BaseDataOperation()
        {
            this.Repository = SetRepository();
        }
        #region Get Operations
        public virtual TEntity Get(Expression<Func<TEntity, bool>> predicate)
        {
            if (Repository is ISelectableRepository<TEntity, TKey> selectableRepository)
            {
                try
                {
                    return selectableRepository.Get(predicate);
                }
                catch (Exception ex)
                {
                    throw new DatabaseOperationException(new DatabaseOperationExceptionModel()
                    {
                    }, ex);
                }
            }
            throw new DatabaseOperationException(new DatabaseOperationExceptionModel()
            {
                DataSourceName = Repository.ToString() + "-Get",
                Code = ValidationConstans.UNAUTHORIZED_REPO
            });
        }
        public virtual IPagedList<TEntity> GetPagedList<TPargerInputModel>(IPagerInputModel pagerInputModel)
        {
            if (Repository is ISelectableRepository<TEntity, TKey> selectableRepository)
            {
                try
                {
                    return selectableRepository.GetList(pagerInputModel);
                }
                catch (Exception ex)
                {
                    throw new DatabaseOperationException(new DatabaseOperationExceptionModel()
                    {
                        Code = "GetPagedList",
                        OriginalMessage = ex.Message.ToString(),
                        DataSourceName = Repository.ToString() + "-GetPagedList"
                    }, ex);
                }
            }
            throw new DatabaseOperationException(new DatabaseOperationExceptionModel()
            {
                DataSourceName = Repository.ToString() + "-GetPagedList",
                Code = ValidationConstans.UNAUTHORIZED_REPO
            });
        }
        public virtual IQueryable<TEntity> GetList(Expression<Func<TEntity, bool>> predicate)
        {
            if (Repository is ISelectableRepository<TEntity, TKey> selectableRepository)
            {
                try
                {
                    if (predicate == null)
                        return selectableRepository.GetList();
                    return selectableRepository.GetList(predicate);
                }
                catch (Exception ex)
                {
                    throw new DatabaseOperationException(new DatabaseOperationExceptionModel()
                    {
                        Code = "GetList",
                        OriginalMessage = ex.Message.ToString(),
                        DataSourceName = Repository.ToString() + "-GetList"
                    }, ex);
                }
            }
            throw new DatabaseOperationException(new DatabaseOperationExceptionModel()
            {
                DataSourceName = Repository.ToString() + "-GetList",
                Code = ValidationConstans.UNAUTHORIZED_REPO
            });
        }
        public virtual List<TEntity> GetList()
        {
            //Her zaman az parametre olan, daha çok parametre alanı kullanır.
            return GetList(s => s.Id.Equals(s.Id)).OrderByDescending(s => s.Id).ToList();
        }
        #endregion
        #region Insert Operations
        public virtual TEntity Insert(TEntity entity)
        {
            if (Repository is IInsertableRepository<TEntity, TKey> insertableRepository)
            {
                try
                {
                    return insertableRepository.Insert(entity);
                }
                catch (Exception ex)
                {
                    throw new DatabaseOperationException(new DatabaseOperationExceptionModel()
                    {
                        Code = "Insert",
                        OriginalMessage = ex.Message.ToString(),
                        DataSourceName = Repository.ToString() + "-Insert"
                    }, ex);
                }

            }
            throw new DatabaseOperationException(new DatabaseOperationExceptionModel()
            {
                DataSourceName = Repository.ToString() + "-Insert",
                Code = ValidationConstans.UNAUTHORIZED_REPO
            });
        }
        #endregion
        #region Update Operations
        public virtual TEntity Update(TEntity entity)
        {

            if (Repository is IUpdatableRepository<TEntity, TKey> updatableRepository)
            {
                try
                {
                    return updatableRepository.Update(entity);
                }
                catch (Exception ex)
                {
                    throw new DatabaseOperationException(new DatabaseOperationExceptionModel()
                    {
                        Code = "Update",
                        OriginalMessage = ex.Message.ToString(),
                        DataSourceName = Repository.ToString() + "-Update"
                    }, ex);
                }
            }
            throw new DatabaseOperationException(new DatabaseOperationExceptionModel()
            {
                DataSourceName = Repository.ToString() + "-Update",
                Code = ValidationConstans.UNAUTHORIZED_REPO
            });
        }
        #endregion
        #region Delete Operations
        public virtual bool Delete(TEntity entity)
        {
            if (entity == null)
                return false;

            if (Repository is IDeletableRepository<TEntity, TKey> deletableRepository)
            {
                try
                {
                    return deletableRepository.Delete(entity);
                }
                catch (Exception ex)
                {
                    throw new DatabaseOperationException(new DatabaseOperationExceptionModel()
                    {
                        Code = "Delete",
                        OriginalMessage = ex.Message.ToString(),
                        DataSourceName = Repository.ToString() + "-Delete"
                    }, ex);
                }
            }
            throw new DatabaseOperationException(new DatabaseOperationExceptionModel()
            {
                DataSourceName = Repository.ToString() + "-Delete",
                Code = ValidationConstans.UNAUTHORIZED_REPO
            });
        }
        public virtual bool Delete(TKey id)
        {
            TEntity entity = Get(s => s.Id.Equals(id));
            return Delete(entity);
        }
        #endregion
        #region Count Operations
        public virtual int Count(Expression<Func<TEntity, bool>> predicate)
        {
            if (Repository is ISelectableRepository<TEntity, TKey> selectableRepository)
            {
                try
                {
                    return selectableRepository.Count(predicate);
                }
                catch (Exception ex)
                {
                    throw new DatabaseOperationException(new DatabaseOperationExceptionModel()
                    {
                    }, ex);
                }
            }
            throw new DatabaseOperationException(new DatabaseOperationExceptionModel()
            {
                DataSourceName = Repository.ToString() + "-Count",
                Code = ValidationConstans.UNAUTHORIZED_REPO
            });
        }
        #endregion
        #region Contains Operations
        public virtual bool Contains(Expression<Func<TEntity, bool>> predicate)
        {
            if (Repository is ISelectableRepository<TEntity, TKey> selectableRepository)
            {
                try
                {
                    return selectableRepository.Contains(predicate);
                }
                catch (Exception ex)
                {
                    throw new DatabaseOperationException(new DatabaseOperationExceptionModel()
                    {
                    }, ex);
                }
            }
            throw new DatabaseOperationException(new DatabaseOperationExceptionModel()
            {
                DataSourceName = Repository.ToString() + "-Count",
                Code = ValidationConstans.UNAUTHORIZED_REPO
            });
        }
        #endregion
    }
}
