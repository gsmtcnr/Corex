using Corex.Model.Derived.EntityModel;
using Corex.Model.Infrastructure;
using System;

namespace Corex.Operation.Manager
{
    public interface IOperationManager<TEntity, TModel, TPagerInputModel, TKey> : ISingletonDependecy
       where TEntity : class, IEntityModel<TKey>, new()
       where TModel : class, IModel<TKey>, new()
       where TPagerInputModel : class, IPagerInputModel, new()
    {
        public IResultModel Delete(int id);
        public IResultModel Delete(TModel model);
        public IResultObjectModel<TModel> Get(Guid uniqId);
        public IResultObjectModel<TModel> Get(int id);
        public IResultObjectPagedListModel<TModel> GetList(IPagerInputModel pagerInputModel);
        public IResultObjectModel<TModel> Insert(TModel dto);
        public IResultObjectModel<TModel> Update(TModel dto);
        public IResultObjectModel<TModel> SaveChanges(TModel dto);
    }
}
