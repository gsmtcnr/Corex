using System.Collections.Generic;

namespace Corex.Model.Infrastructure
{
    public interface IResultObjectPagedListModel<T> : IResultPagedListModel, IResultModel
         where T : class, new()
    {
        List<T> Data { get; set; }
    }
}
