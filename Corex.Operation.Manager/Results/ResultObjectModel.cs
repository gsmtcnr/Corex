using Corex.Model.Infrastructure;
using System.Collections.Generic;

namespace Corex.Operation.Manager.Results
{
    public class ResultObjectModel<TData> : BaseResultObjectModel<TData>, IResultObjectModel<TData>
      where TData : class, new()
    {
        public ResultObjectModel(TData data)
        {
            Data = data;
            IsSuccess = true;
        }
        public ResultObjectModel()
        {
            IsSuccess = true;
        }
    }
}
