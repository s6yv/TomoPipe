using Newtonsoft.Json;

namespace Platom.Protocol.Schema
{
    /// <summary>
    /// Klasa reprezentująca nagłówek schematu walidującego.
    /// </summary>
    public class SchemaHeader
    {
        /// <summary>
        /// Nazwa usługi generującej komunikaty, opisane przed dany schemat walidacyjny.
        /// </summary>
        [JsonProperty("service")] public string ServiceName { get; set; }

        /// <summary>
        /// Nazwa danego schematu walidacyjnego.
        /// </summary>
        [JsonProperty("name")] public string SchemaName { get; set; }

        /// <summary>
        /// Nazwa kanału, którym przesyłane są komunikaty, opisane przez dany schemat walidacyjny.
        /// </summary>
        [JsonProperty("channel")] public string PublishingChannelName { get; set; }

        /// <summary>
        /// Opis schematu walidacyjnego.
        /// </summary>
        [JsonProperty("description")] public string Description { get; set; }

        /// <summary>
        /// Opis mapowania pól komunkatu, danego schematem, na struktury bazy danych.
        /// </summary>
        [JsonProperty("mapping")] public SchemaMapping Mapping { get; set; }
    }
}