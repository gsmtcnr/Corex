using Corex.Model.Infrastructure;
using PagedList.Core;
using System.Collections.Generic;

namespace Corex.Operation.Manager.Results
{
    public class ResultObjectPagedListModel<TData> : BaseResultObjectPagedListModel<TData>, IResultObjectPagedListModel<TData>
      where TData : class, new()
    {
        public ResultObjectPagedListModel()
        {

        }
        public ResultObjectPagedListModel(IPagedList metaData, List<TData> data) : base(metaData, data)
        {
        }

        public ResultObjectPagedListModel(IResultPagedListModel pagedListModel, List<TData> data) : base(pagedListModel, data)
        {
        }
    }
}
