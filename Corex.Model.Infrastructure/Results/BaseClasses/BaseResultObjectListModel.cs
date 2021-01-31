using System.Collections.Generic;
using System.Linq;

namespace Corex.Model.Infrastructure
{
    public abstract class BaseResultObjectListModel<T> : BaseResultModel, IResultObjectListModel<T>
     where T : class, new()
    {
        public BaseResultObjectListModel()
        {
            Data = new List<T>();
        }
        public BaseResultObjectListModel(List<T> data)
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
        public List<T> Data { get; set; }
    }
}
