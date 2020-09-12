using Platom.Protocol.Schema.Validators;

namespace Platom.Protocol.Schema.Loader
{
    public class Entry
    {
        public SchemaValidator Validator { get; private set; }
        public string FileName { get; private set; }

        public string SchemaName => this.Validator.SchemaName;
        public string ServiceName => this.Validator.ServiceName;

        public Entry(string fileName, SchemaValidator schemaValidator)
        {
            this.Validator = schemaValidator;
            this.FileName = fileName;
        }
    }
}