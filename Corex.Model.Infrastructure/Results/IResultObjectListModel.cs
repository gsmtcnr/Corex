using System.Collections.Generic;

namespace Corex.Model.Infrastructure
{
    public interface IResultObjectListModel<TData> : IResultModel
          where TData : class, new()
    {
        List<TData> Data { get; set; }
    }
}
