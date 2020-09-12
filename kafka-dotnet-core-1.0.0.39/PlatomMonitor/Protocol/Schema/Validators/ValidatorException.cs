using System;
using System.Runtime.Serialization;

namespace Platom.Protocol.Schema.Validators
{
    [Serializable]
    public class ValidatorException : Exception
    {
        //
        // For guidelines regarding the creation of new exception types, see
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
        // and
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
        //

        public ValidatorException()
        {
        }

        public ValidatorException(string message) : base(message)
        {
        }

        public ValidatorException(string message, Exception inner) : base(message, inner)
        {
        }

        protected ValidatorException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}