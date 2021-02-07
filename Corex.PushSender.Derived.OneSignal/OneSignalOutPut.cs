using Corex.Push.Infrastructure;

namespace Corex.Push.Derived.OneSignal
{
    public class OneSignalOutPut : IPushOutput
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}
