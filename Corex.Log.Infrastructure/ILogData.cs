using System;

namespace Corex.Log.Infrastructure
{
    public interface ILogData
    {
        DateTime CreatedDate { get; set; }
        string Log { get; set; }
    }
}
