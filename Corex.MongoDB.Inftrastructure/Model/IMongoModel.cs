using Corex.Model.Infrastructure;
using System;

namespace Corex.MongoDB.Inftrastructure
{
    public interface IMongoModel : IModel<Guid>   /// Bir relation DB olmadığı için Key değerini Guid olarak kullanıyoruz..
    {
        //MongoDB özelinde olmazsa olmaz property varsa buraya ekleyebiliriz.
    }
}
