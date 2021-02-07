namespace Corex.SmsSender.Infrastructure
{
    public interface ISmsInput
    {
        string Phone { get; set; }
        string Text { get; set; }
    }
}
