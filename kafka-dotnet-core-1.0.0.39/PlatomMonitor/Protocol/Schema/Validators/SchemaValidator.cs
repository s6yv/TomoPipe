using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Platom.Protocol;
using Platom.Protocol.Schema;
using Platom.Protocol.Schema.Exceptions;
using Platom.Protocol.Schema.Validators;

namespace Platom.Protocol.Schema.Validators
{
    public class SchemaValidator
    {
        public string SchemaText { get; private set; }
        public SchemaDocument SchemaDocument { get; private set; }

        public string SchemaName => SchemaDocument.Header.SchemaName;
        public string ServiceName => SchemaDocument.Header.ServiceName;

        public SchemaValidator(string schemaContent)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
                // $type no longer needs to be first
                MetadataPropertyHandling = MetadataPropertyHandling.ReadAhead,
                MissingMemberHandling = MissingMemberHandling.Error

            };

            Platom.Protocol.Schema.SchemaDocument jschema = null;
            try
            {
                jschema = JsonConvert.DeserializeObject<Platom.Protocol.Schema.SchemaDocument>(schemaContent, settings);
            }
            catch (JsonException jre)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("Błąd struktury JSON dokumentu walidującego: ");
                sb.Append(jre.Message);
                throw new ValidatorException(sb.ToString(), jre);
            }

            ValidateSchemaDocument(jschema);

            this.SchemaText = schemaContent;
            this.SchemaDocument = jschema;
        }

        /// <summary>
        /// Metoda walidująca komunikat sieci Platom, dany w strukturze JSON. 
        /// W przypadku błędów generowany jest wyjątek <see cref="ValidatorException"/>. Dla błędów składniowych należy sprawdzić pole InnerException.
        /// </summary>
        /// <param name="message">Treść komunikatu</param>
        /// <exception cref="ValidatorException">Wyjątek generowany w w przypadku napotkania na błąd skłądniowy komunikatu lub niezgodność ze schematem walidacyjnym.</exception>
        public void ValidateMessage(string message)
        {
            CommonMessage_DictionaryPayload sm = SchemaValidator.DeserializeAsDictionaryPayload(message);
            InternalValidateMessage(sm);
        }

        public static CommonMessage_DictionaryPayload DeserializeAsDictionaryPayload(string messageContent)
        {
            JsonSerializerSettings jss = new JsonSerializerSettings()
            {
                // Używaj atrybutu DefaultValueAttribute
                DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate,
                DateParseHandling = DateParseHandling.None
            };

            try
            {
                CommonMessage_DictionaryPayload sm = null;
                sm = JsonConvert.DeserializeObject<CommonMessage_DictionaryPayload>(messageContent, jss);
                return sm;
            }
            catch (JsonException jre)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("Błąd struktury JSON w treści komunikatu: ");
                sb.Append(jre.Message);
                throw new ValidatorException(sb.ToString(), jre);
            }
        }

        #region Walidacja schematu

        private void ValidateSchemaDocument(SchemaDocument document)
        {
            // sprawdzanie nagłówka
            ValidateHeader(document.Header);

            // Weryfikacja elementów schematu
            ValidateEntries(document.Entries);

        }

        private void ValidateEntries(List<SchemaFieldEntry> fields)
        {
            if (fields == null || fields.Count == 0)
                throw new ValidatorException("Lista pól schematu nie może być pusta");

            // czy nazwy pól są unikalne?
            if (fields.Count != fields.Select(x => x.Name).Distinct().Count())
                throw new ValidatorException("Nazwy pól w schemacie nie są unikalne");

            // sprawdź nazwy
            foreach (SchemaFieldEntry field_entry in fields)
                ValidateSingleFieldEntry(field_entry);
        }

        private void ValidateSingleFieldEntry(SchemaFieldEntry entry)
        {
            if (entry == null)
                throw new ValidatorException("Pole schematu nie może być pusty (null)");

            // Weryfikacja nazwy pola
            EntryValidator.Validate(entry.Name, "Błąd w nazwie pola", new SchemaFieldNameValidator());

            // Weryfikacja opisu schematu
            EntryValidator.Validate(entry.Description, "Bład w opisie pola", new SchemaFieldDescriptorValidator());

            // Weryfikacja opisu typu danych
            EntryValidator.Validate(entry.Type, $"Bład w typie pola '{entry.Name}", new SchemaTypeValidator(entry));


            // Walidacja mapowania
            if (entry.Mapping != null)
                ValidateSingleFieldMapping(entry, entry.Mapping);


        }

        private void ValidateSingleFieldMapping(SchemaFieldEntry entry, StorageMapping mappint)
        {
            // TODO: uzupełnić po określenie sposobu zapisu danych do bazy
        }


        private void ValidateHeader(SchemaHeader header)
        {
            // Sprawdź, czy nagłówek został wczytany
            if (header == null)
                throw new ValidatorException("Brak nagłówka 'header'");

            // Weryfikacja nazwy schematu
            EntryValidator.Validate(header.SchemaName, "Błąd w nazwie schematu", new SchemaNameValidator());

            // Weryfikacja nazwy usługi, dla której istnieje ten schemat
            EntryValidator.Validate(header.ServiceName, "Bład w nazwie usługi", new ServiceNameValidator(true));

            // Weryfikacja opisu schematu
            EntryValidator.Validate(header.Description, "Bład w opisie schematu", new SchemaDescriptorValidator());

            // Weryfikacja nazwy kanału komunikataów objętych danym schematem
            EntryValidator.Validate(header.PublishingChannelName, "Bład nazwie kanału dla schematu", new SchemaChannelNameValidator());

            // Weryfikacja mapowania
            if (header.Mapping != null)
            {
                ValidateMapping(header.Mapping);
            }
        }

        private void ValidateMapping(SchemaMapping mapping)
        {
            //todo: uzupełnić
        }

        #endregion

        #region Walidacja komunikatu

        private void InternalValidateMessage(CommonMessage_DictionaryPayload msg)
        {

            ////
            //// Walidacja pola 'sequence'
            //if (msg.Sequence == null)
            //    throw new ValidatorException("Brak pola 'sequence'");

            //EntryValidator.Validate(msg.Sequence.ServiceName, "Bład w nazwie usługi", new ServiceNameValidator(false));
            //EntryValidator.Validate(msg.Sequence.ChannelName, "Bład w nazwie kanału", new SchemaChannelNameValidator());
            //EntryValidator.Validate(msg.Sequence.SchemaName, "Bład w nazwie schematu walidacyjnego", new SchemaNameValidator());

            //if (msg.Sequence.SchemaName != this.SchemaDocument.Header.SchemaName)
            //    throw new ValidatorException("Nazwa schematu walidacyjnego oraz wartość pola 'sequence/schema' jest niezgodna");
            //if (this.SchemaDocument.Header.ServiceName != "*" && msg.Sequence.ServiceName != this.SchemaDocument.Header.ServiceName)
            //    throw new ValidatorException("Nazwa schematu walidacyjnego oraz wartość pola 'sequence/service' jest niezgodna");

            //if (!msg.Sequence.SequenceNumber.HasValue)
            //    throw new ValidatorException("Brak wartości pola numeru sekwencji 'number'");
            //if (msg.Sequence.SequenceNumber <= 0)
            //    throw new ValidatorException("Brak wartości pola numeru sekwencji 'number' musi być większa od zera");
            ValidateMessageSequence(msg);

            if (msg.Sequence.SchemaName != this.SchemaDocument.Header.SchemaName)
                throw new ValidatorException("Nazwa schematu walidacyjnego oraz wartość pola 'sequence/schema' jest niezgodna");
            if (this.SchemaDocument.Header.ServiceName != "*" && msg.Sequence.ServiceName != this.SchemaDocument.Header.ServiceName)
                throw new ValidatorException("Nazwa schematu walidacyjnego oraz wartość pola 'sequence/service' jest niezgodna");


            //
            // Walidacja pola 'payload'
            if (msg.Payload == null)
                throw new ValidatorException("Brak pola 'payload'");

            // sprwdź, czy liczba pól się zgdza
            string[] schema_fields = this.SchemaDocument.Entries.Select(x => x.Name).OrderBy(y => y).ToArray();
            string[] message_fields = msg.Payload.Keys.OrderBy(y => y).ToArray();

            if (schema_fields.Length != message_fields.Length)
                throw new ValidatorException("Liczba pól w schemacie walidacyjnym i komunikacie jest różna");

            // czy to są te same nazwy?
            if (!schema_fields.SequenceEqual(message_fields))
                throw new ValidatorException("Nazwy pol w schemacie walidacyjnym i komunikacie różnią się od siebie");

            // sprawdzanie kolejnych pól
            foreach (SchemaFieldEntry entry in this.SchemaDocument.Entries)
            {
                object value = msg.Payload[entry.Name];
                entry.Type.ValidateValue(value);
            }


        }


        public static void ValidateMessageSequence(CommonMessage msg)
        {

            //
            // Walidacja pola 'sequence'
            if (msg.Sequence == null)
                throw new ValidatorException("Brak pola 'sequence'");

            EntryValidator.Validate(msg.Sequence.ServiceName, "Bład w nazwie usługi", new ServiceNameValidator(false));
            EntryValidator.Validate(msg.Sequence.ChannelName, "Bład w nazwie kanału", new SchemaChannelNameValidator());
            EntryValidator.Validate(msg.Sequence.SchemaName, "Bład w nazwie schematu walidacyjnego", new SchemaNameValidator());

            if (!msg.Sequence.SequenceNumber.HasValue)
                throw new ValidatorException("Brak wartości pola numeru sekwencji 'number'");
            if (msg.Sequence.SequenceNumber <= 0)
                throw new ValidatorException("Brak wartości pola numeru sekwencji 'number' musi być większa od zera");
        }

        #endregion


    }
}