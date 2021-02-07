using Corex.Push.Infrastructure;

namespace Corex.Push.Derived.OneSignal
{
    public class OneSignalInput : IPushInput
    {
        public string To { get; set; }
        public string SubTitle { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public byte Priority { get; set; }
        public string Payload { get; set; }
    }
}
