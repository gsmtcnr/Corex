namespace Corex.ExceptionHandling.Infrastructure.Models
{
    public class BusinesOperationExceptionModel : BaseExceptionModel
    {
        public string ClassName { get; set; }
        public string MethodName { get; set; }

        public override string GetUFMessageCreate()
        {
            return string.Format("Class:{0} - Method:{1}", ClassName, MethodName);
        }
    }
}