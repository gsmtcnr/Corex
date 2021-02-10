using System.Linq;

namespace Corex.Model.Infrastructure
{
    public abstract class BaseResultObjectModel<TData> : BaseResultModel, IResultObjectModel<TData>
    where TData : class, new()
    {
        public BaseResultObjectModel()
        {
            Data = new TData();
        }
        public BaseResultObjectModel(TData data)
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
        public TData Data { get; set; }
    }
}
