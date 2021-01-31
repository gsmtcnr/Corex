namespace Corex.Model.Infrastructure
{
    public interface IResultObjectModel<T> : IResultModel
   where T : class, new()
    {
        T Data { get; set; }
    }
}
