using Corex.Model.Infrastructure;

namespace Corex.CipherEncryption.Infrastructure
{
    public interface ICipherEncryption : ISingletonDependecy
    {
        string Encrypt(string text, string password);
        string Decrypt(string text, string password);
    }
}
