using Corex.Data.Infrastructure;
using Corex.Model.Derived.EntityModel;
using Corex.Model.Infrastructure;
using Corex.Utility.Infrastructure;
using Microsoft.EntityFrameworkCore;
using PagedList.Core;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Corex.Data.Derived.EntityFramework
{
    public abstract class BaseEntityRepository<OC, TEntityModel, TKey>
        :
        ISelectableRepository<TEntityModel, TKey>,
        IInsertableRepository<TEntityModel, TKey>,
        IDeletableRepository<TEntityModel, TKey>,
        IUpdatableRepository<TEntityModel, TKey>
        where TEntityModel : class, IEntityModel<TKey> where OC : BaseEntityContext, new()
    {
        public OC _context;
        public readonly DbSet<TEntityModel> _dbSet;
        private IQueryable<TEntityModel> _queryEntity;
        public BaseEntityRepository()
        {
            _context = new OC();
            _dbSet = _context.Set<TEntityModel>();
            _queryEntity = _dbSet.Where(s => s.IsDeleted == false).AsNoTracking();
        }

        public bool Delete(TEntityModel item)
        {
            item.IsDeleted = true;
            item.DeletedTime = DateTime.Now;
            TrackingControl(item);
            _dbSet.Attach(item);
            _context.Entry(item).State = EntityState.Modified;
            _context.SaveChanges();
            return true;//işlem sırasında hata olmadıysa true..
        }

        public TEntityModel Get(Expression<Func<TEntityModel, bool>> predicate)
        {
            return _queryEntity.Where(predicate).FirstOrDefault();
        }
        public bool Contains(Expression<Func<TEntityModel, bool>> predicate)
        {
            return _queryEntity.Any(predicate);
        }
        public int Count(Expression<Func<TEntityModel, bool>> predicate)
        {
            return _queryEntity.Count(predicate);
        }
        public IQueryable<TEntityModel> GetList(Expression<Func<TEntityModel, bool>> predicate)
        {
            return _queryEntity.Where(predicate).AsNoTracking();
        }
        public IQueryable<TEntityModel> GetList()
        {
            return _queryEntity.AsNoTracking();
        }
        public TEntityModel Insert(TEntityModel item)
        {
            item.CreatedTime = DateTime.Now;
            var insertItem = _dbSet.Add(item);
            _context.SaveChanges();
            return insertItem.Entity;
        }

        public TEntityModel Update(TEntityModel item)
        {
            item.UpdatedTime = DateTime.Now;
            TrackingControl(item);
            _dbSet.Attach(item);
            _context.Entry(item).State = EntityState.Modified;
            _context.SaveChanges();
            return item;
        }
        protected virtual IQueryable<TEntityModel> ListExpression(IQueryable<TEntityModel> query, IPagerInputModel input)
        {
            return query;
        }
        protected virtual IQueryable<TEntityModel> OrderByExpression(IQueryable<TEntityModel> query, IPagerInputModel input)
        {
            if (!string.IsNullOrEmpty(input.SortColumn))
            {
                
                if (input.SortDescending)
                    query = query.OrderByDescending(input.SortColumn);
                else
                    query = query.OrderBy(input.SortColumn);
            }
            return query;
        }
        protected virtual IQueryable<TEntityModel> IsActiveExpression(IPagerInputModel pagerInputModel, IQueryable<TEntityModel> query)
        {
            if (pagerInputModel.IsActive.HasValue)
                query = query.Where(s => s.IsActive == pagerInputModel.IsActive.Value);
            return query;
        }
        public IPagedList<TEntityModel> GetList(IPagerInputModel pagerInputModel)
        {
            var query = ListExpression(_queryEntity, pagerInputModel);
            query = OrderByExpression(query, pagerInputModel);
            query = IsActiveExpression(pagerInputModel, query);
            var paged = query.ToPagedList(pagerInputModel.PageNumber, pagerInputModel.PageSize);
            return paged;
        }

        #region Private Methods
        protected void TrackingControl(TEntityModel item)
        {
            bool tracking = _context.ChangeTracker.Entries<TEntityModel>().Any(s => s.Entity.Id.Equals(item.Id));
            if (tracking)
                _context.ChangeTracker.Entries<TEntityModel>().FirstOrDefault(s => s.Entity.Id.Equals(item.Id)).State = EntityState.Detached;
        }
        #endregion
    }
}
