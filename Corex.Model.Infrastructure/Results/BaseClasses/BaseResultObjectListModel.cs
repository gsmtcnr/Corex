using System.Collections.Generic;
using System.Linq;

namespace Corex.Model.Infrastructure
{
    public abstract class BaseResultObjectListModel<TData> : BaseResultModel, IResultObjectListModel<TData>
     where TData : class, new()
    {
        public BaseResultObjectListModel()
        {
            Data = new List<TData>();
        }
        public BaseResultObjectListModel(List<TData> data)
        {
            Data = data;
        }
        public override void SetResult()
        {
            if (Data == null)
            {
                IsSuccess = false;
                Message = "Data is null";
            }
            else if (Messages.Any())
            {
                IsSuccess = false;
                Message = "Messages for detail";
            }
            else if (Data.Count == 0)
            {
                IsSuccess = false;
                Message = "Data is empty";
            }
            else
                IsSuccess = true;
        }
        public List<TData> Data { get; set; }
    }
}
