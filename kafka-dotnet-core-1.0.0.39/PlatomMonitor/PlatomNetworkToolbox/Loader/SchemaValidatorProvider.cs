using System.Collections.Generic;
using System.IO;
using Platom.Protocol;
using Platom.Protocol.Schema.Validators;

namespace Platom.Protocol.Schema.Loader
{
    public class SchemaValidatorProvider
    {
        private List<Entry> items;

        public SchemaValidatorProvider()
        {
            this.items = new List<Entry>();
        }

        public int Count => this.items.Count;

        public SchemaValidator LoadSchemaFromFile(string filePath, LoadMode loadMode)
        {
            try
            {
                string content = File.ReadAllText(filePath);
                SchemaValidator sv = new SchemaValidator(content);

                Entry existing = this.items.Find(x => x.SchemaName == sv.SchemaName);
                if (existing != null)
                    throw new SchemaValidatorProviderException(
                        $"Schemat {sv.SchemaName} wczytywany z pliku {filePath} został już wcześniej wczytany z {existing.FileName}.");

                if (loadMode == LoadMode.LoadAndStore)
                    this.items.Add(new Entry(filePath, sv));
                return sv;
            }
            catch (ValidatorException ve)
            {
                throw new SchemaValidatorProviderException($"Błąd wczytywania schematu z pliku {filePath}: " + ve.Message, ve);
            }
            catch (IOException ioe)
            {
                throw new SchemaValidatorProviderException($"Błąd wejścia/wyjścia z pliku {filePath}: " + ioe.Message, ioe);
            }
        }



        public SchemaValidator TryMatchValidator(string messageContent)
        {
            try
            {
                CommonMessage_DictionaryPayload cm = SchemaValidator.DeserializeAsDictionaryPayload(messageContent);
            }
            catch (ValidatorException ve)
            {
                // to można zignorować, np. błąd składni
                return null;
            }

            foreach (Entry entry in items)
            {
                try
                {
                    entry.Validator.ValidateMessage(messageContent);
                }
                catch (ValidatorException ve)
                {
                    // Błąd walidacji, następny walidator
                    continue;
                }

                return entry.Validator;
            }

            // Nie udało się znaleźć pasującego walidatora
            return null;
        }
    }
}