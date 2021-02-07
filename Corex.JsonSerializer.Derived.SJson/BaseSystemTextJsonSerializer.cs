using Corex.JsonSerializer.Infrastructure;
using System;

namespace Corex.JsonSerializer.Derived.SJson
{
    public abstract class BaseSystemTextJsonSerializer : IJsonSerializer
    {
        public T DeSerializeObject<T>(string data)
        {
            return System.Text.Json.JsonSerializer.Deserialize<T>(data);
        }

        public string SerializeObject<T>(T data)
        {
            return System.Text.Json.JsonSerializer.Serialize<T>(data);
        }
    }
}
