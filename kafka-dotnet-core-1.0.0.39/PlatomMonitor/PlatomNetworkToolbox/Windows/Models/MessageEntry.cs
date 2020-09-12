using System.IO;

namespace PlatomMonitor.Windows.Models
{
    public class MessageEntry
    {
        /// <summary> Nazwa pliku z komunikatem, na dysku loklanym. </summary>
        public string FileName { get; set; }

        /// <summary> Nazwa schematu poprawnie weryfikującego dany komunikat. </summary>
        public string SchemaName { get; set; }

        /// <summary> Nazwa usługi generującej dany komunikat. </summary>
        public string ServiceName { get; set; }

        /// <summary> Nazwa kanału, którym przesyłany jest dany komunikat. </summary>
        public string ChannelName { get; set; }

        /// <summary> Treść komunikatu. </summary>
        public string Text { get; set; }

        public override string ToString() => $"Komunikat od [{ServiceName}] w kanale [{ChannelName}], plik [{Path.GetFileName(FileName)}] z [{Path.GetDirectoryName(FileName)}]";

    }
}