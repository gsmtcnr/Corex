using Corex.TemplateRender.Infrastructure;
using HandlebarsDotNet;

namespace Corex.TemplateRender.Derived.HBars
{
    public abstract class BaseHandleBarsRender : ITemplateRender
    {
        public string Compile(string source, object data)
        {
            HandlebarsTemplate<object, object> template = Handlebars.Compile(source);
            string result = template(data);
            return result;
        }
    }
}
