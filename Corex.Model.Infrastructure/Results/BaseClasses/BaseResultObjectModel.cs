using System.Linq;

namespace Corex.Model.Infrastructure
{
    public abstract class BaseResultObjectModel<T> : BaseResultModel, IResultObjectModel<T>
   where T : class, new()
    {
        public BaseResultObjectModel()
        {
            Data = new T();
        }
        public BaseResultObjectModel(T data)
        {
            Data = data;
        }
        public override void SetResult()
        {
            if (Data == null || Messages.Any())
            {
                IsSuccess = false;
                Message = "Data is null";
            }
            else
                IsSuccess = true;
        }
        public T Data { get; set; }
    }
}
