using Corex.Model.Derived.EntityModel;
using Corex.Model.Infrastructure;

namespace Corex.Operation.Manager
{
    public interface IOperationManager<TKey, TEntity, TModel> : ISingletonDependecy
       where TEntity : class, IEntityModel<TKey>, new()
       where TModel : class, IModel<TKey>, new()

    {
        public IResultModel Delete(TKey id);
        public IResultModel Delete(TModel model);
        public IResultObjectModel<TModel> Get(TKey id);
        public IResultObjectPagedListModel<TModel> GetList(IPagerInputModel pagerInputModel);
        public IResultObjectModel<TModel> Insert(TModel dto);
        public IResultObjectModel<TModel> Update(TModel dto);
        public IResultObjectModel<TModel> SaveChanges(TModel dto);
    }
}
