using Corex.Model.Derived.EntityModel;
using Corex.Model.Infrastructure;
using Corex.Operation.Inftrastructure;

namespace Corex.Operation.Manager
{
    public interface IOperationManager<TKey, TEntity, TModel> : ISingletonDependecy
       where TEntity : class, IEntityModel<TKey>, new()
       where TModel : class, IModel<TKey>, new()

    {
        ICacheSettings CacheSettings { get; set; }
        IResultModel Delete(TKey id);
        IResultModel Delete(TModel model);
        IResultObjectModel<TModel> Get(TKey id);
        IResultObjectPagedListModel<TModel> GetList(IPagerInputModel pagerInputModel);
        IResultObjectModel<TModel> Insert(TModel dto);
        IResultObjectModel<TModel> Update(TModel dto);
        IResultObjectModel<TModel> SaveChanges(TModel dto);
    }
}
