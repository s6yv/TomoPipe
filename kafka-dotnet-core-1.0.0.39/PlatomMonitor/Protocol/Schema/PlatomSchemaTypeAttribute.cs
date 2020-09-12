using System;

namespace Platom.Protocol.Schema
{
    [AttributeUsage(AttributeTargets.Class)]
    public class PlatomSchemaTypeAttribute : Attribute
    {
        public string TypeName { get; set; }

        public PlatomSchemaTypeAttribute(string name)
        {
            this.TypeName = name;
        }
    }
}