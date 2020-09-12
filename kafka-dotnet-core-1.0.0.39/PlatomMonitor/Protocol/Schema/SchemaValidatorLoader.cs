using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using Platom.Protocol.Schema.Validators;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using Platom;
using Platom.Protocol.Schema;
using Platom.Protocol.Schema.Validators;

namespace Platom.Protocol.Schema
{
    /*
    public class SchemaValidatorLoader : IDisposable
    {
        private Dictionary<string, SchemaValidator> schemas;


        /// <summary>
        /// Flaga magazynowania walidatorów; true - ładowarka próbuje wczytaś schemat, jeżeli nie ma go w słowniku; false - każdorazowo próbuje wczytać go z dysku
        /// </summary>
        public bool ValidatorCacheEnabled { get; set; }

        public SchemaValidatorLoader()
        {
            this.schemas = new Dictionary<string, SchemaValidator>();
        }

        public SchemaValidator LoadFromDisk(string schemaName)
        {

            // jeżeli cache jest włączony i zawiera schemat o podanej nazwie, to z niego skorzystaj
            if (ValidatorCacheEnabled && this.schemas.ContainsKey(schemaName))
                return this.schemas[schemaName];

            ;
            //string binary_directory = Path.GetDirectoryName(Environment.GetCommandLineArgs()[0]);
            //string schema_directory = Path.Combine(binary_directory, "schemas");
            string schema_directory = Platom.Configuration.ValidationSchemasRepositoryPath;
            string schema_file = Path.ChangeExtension(schemaName, "json");
            string schema_path = Path.Combine(schema_directory, schema_file);

            string content = null;
            try
            {
                content = File.ReadAllText(schema_path);
            }
            catch (IOException ioe)
            {
                throw new ValidatorException($"Błąd podczas wczytywania schematu walidacyjnego '{schema_path}'.", ioe);
            }

            SchemaValidator sv = new SchemaValidator(content);

            if (ValidatorCacheEnabled)
                this.schemas.Add(schemaName, sv);
            return sv;
        }

        public void Dispose()
        {
            this.schemas.Clear();
        }
    }
    */
}
