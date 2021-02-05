using Corex.Model.Infrastructure;

namespace Corex.TemplateRender.Infrastructure
{
    public interface ITemplateRender : ISingletonDependecy
    {
        string Compile(string source, object data);
    }
}
