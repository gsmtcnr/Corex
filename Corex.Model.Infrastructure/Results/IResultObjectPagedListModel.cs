using System.Collections.Generic;

namespace Corex.Model.Infrastructure
{
    public interface IResultObjectPagedListModel<TData> : IResultPagedListModel, IResultModel
         where TData : class, new()
    {
        List<TData> Data { get; set; }
    }
}
