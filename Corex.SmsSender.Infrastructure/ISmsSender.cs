using Corex.Model.Infrastructure;

namespace Corex.SmsSender.Infrastructure
{
    public interface ISmsSender : ISingletonDependecy
    {
        ISmsOutput Send(ISmsInput sms);
    }
}
