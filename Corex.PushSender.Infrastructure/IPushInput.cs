namespace Corex.Push.Infrastructure
{
    public interface IPushInput
    {
        string To { get; set; }
        string SubTitle { get; set; }
        string Title { get; set; }
        string Message { get; set; }
        byte Priority { get; set; }
        string Payload { get; set; }
    }
}
