namespace Corex.Model.Infrastructure
{
    public abstract class BaseResultMessage : IResultMessage
    {
        public string Message { get; set; }
        public string Code { get; set; }
    }
}
