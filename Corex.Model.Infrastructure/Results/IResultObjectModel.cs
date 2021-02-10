namespace Corex.Model.Infrastructure
{
    public interface IResultObjectModel<TData> : IResultModel
          where TData : class, new()
    {
        TData Data { get; set; }
    }
}
