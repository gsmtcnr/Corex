using Corex.Model.Infrastructure;
using System.Threading.Tasks;

namespace Corex.EmailSender.Infrastructure
{
    public interface IEmailSender : ISingletonDependecy
    {
        Task<IEmailOutput> SendAsync(IEmailInput emailInput);
    }
}
