namespace Corex.SmsSender.Infrastructure
{
    public interface ISmsOutput
    {
        bool IsSuccess { get; set; }
        string Message { get; set; }
    }
}
