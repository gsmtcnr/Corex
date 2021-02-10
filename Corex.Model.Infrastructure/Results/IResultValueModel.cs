namespace Corex.Model.Infrastructure
{
    public interface IResultValueModel<TValue> : IResultModel
    {
        TValue Value { get; set; }
    }
}
