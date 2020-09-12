using System;
using System.Runtime.Serialization;

namespace PlatomMonitor.PROTO
{
    [Serializable]
    public class ServicesActivityMonitorException : Exception
    {
        //
        // For guidelines regarding the creation of new exception types, see
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
        // and
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
        //

        public ServicesActivityMonitorException()
        {
        }

        public ServicesActivityMonitorException(string message) : base(message)
        {
        }

        public ServicesActivityMonitorException(string message, Exception inner) : base(message, inner)
        {
        }

        protected ServicesActivityMonitorException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}