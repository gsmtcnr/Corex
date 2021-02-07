namespace Corex.Push.Infrastructure
{
    public interface IPushOutput
    {
        bool IsSuccess { get; set; }
        string Message { get; set; }
    }
}
