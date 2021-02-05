using Corex.Model.Infrastructure;

namespace Corex.JsonSerializer.Infrastructure
{
    public interface IJsonSerializer : ISingletonDependecy
    {
        string SerializeObject<T>(T data);
        T DeSerializeObject<T>(string data);
    }
}
