using System.Collections.Generic;

namespace Corex.Model.Infrastructure
{
    public interface IResultObjectListModel<T> : IResultModel
          where T : class, new()
    {
        List<T> Data { get; set; }
    }
}
