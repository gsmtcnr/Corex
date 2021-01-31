using System.Collections.Generic;

namespace Corex.ExceptionHandling.Manager.Models
{
    public class ExceptionMessageModel
    {
        public ExceptionMessageModel()
        {
            Messages = new List<ExceptionMessage>();
        }
        //public string OriginalMessage { get; set; }
        public List<ExceptionMessage> Messages { get; set; }
      
    }
}
