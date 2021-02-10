using Corex.Model.Infrastructure;
using System.Collections.Generic;

namespace Corex.Operation.Manager.Results
{
    public class ResultObjectListModel<TData> : BaseResultObjectListModel<TData>, IResultObjectListModel<TData>
    where TData : class, new()
    {
        public ResultObjectListModel()
        {

        }
        public ResultObjectListModel(List<TData> data) : base(data)
        {
        }
    }
}
