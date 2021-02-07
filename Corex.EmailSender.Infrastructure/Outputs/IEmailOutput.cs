namespace Corex.EmailSender.Infrastructure
{
    public interface IEmailOutput
    {
        bool IsSuccess { get; set; }
        string Message { get; set; }
    }
}
