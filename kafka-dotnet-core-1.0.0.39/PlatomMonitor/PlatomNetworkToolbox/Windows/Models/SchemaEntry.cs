using System.Collections.Generic;
using System.IO;

namespace PlatomMonitor.Windows.Models
{
    public class SchemaEntry
    {
        public string FileName { get; set; }
        public string SchemaName { get; set; }
        public string ServiceName { get; set; }

        public List<MessageEntry> Messages { get; }
        public string Text { get; set; }

        public SchemaEntry()
        {
            this.Messages = new List<MessageEntry>();
        }

        public override string ToString() => $"Schemat [{SchemaName}] usługi [{ServiceName}], plik [{Path.GetFileName(FileName)}] z [{Path.GetDirectoryName(FileName)}]";
    }
}