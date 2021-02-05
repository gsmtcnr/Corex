using Corex.JsonSerializer.Infrastructure;
using Newtonsoft.Json;
using System;

namespace Corex.JsonSerializer.Derived.NSoft
{
    public class BaseNewtonsoftSerializer : IJsonSerializer
    {
        public T DeSerializeObject<T>(string data)
        {
            return JsonConvert.DeserializeObject<T>(data, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,

            });
        }
        public string SerializeObject<T>(T data)
        {
            return JsonConvert.SerializeObject(data, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
        }

    }
}
