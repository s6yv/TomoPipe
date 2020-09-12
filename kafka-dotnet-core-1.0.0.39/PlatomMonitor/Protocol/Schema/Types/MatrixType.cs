using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Platom.Protocol.Schema.Validators;

namespace Platom.Protocol.Schema
{
    [PlatomSchemaType("matrix")]
    public class MatrixType : SchemaBaseType
    {
        [JsonProperty("minrows")] public int? MinRows { get; set; }
        [JsonProperty("maxrows")] public int? MaxRows { get; set; }
        [JsonProperty("mincols")] public int? MinColumns { get; set; }
        [JsonProperty("maxcols")] public int? MaxColumns { get; set; }

        [JsonProperty("default")] public object DefaultValue { get; set; }

        [JsonProperty("type"), JsonConverter(typeof(SchemaBaseTypeJsonConverter))]
        public SchemaBaseType Type { get; set; }

        public override void ValidateSchemaInformation()
        {
            // Sprawdzenie wierszy
            if (MaxRows.HasValue && !MinRows.HasValue)
                if (MaxRows.Value < 0)
                    throw new ValidatorException($"Liczba maksimum wierszy musi być >= 0");

            if (!MaxRows.HasValue && MinRows.HasValue)
                if (MinRows.Value < 0)
                    throw new ValidatorException($"Liczba minimum musi być >= 0");

            if (MaxRows.HasValue && MinRows.HasValue)
                if (MinRows.Value > MaxRows.Value)
                    throw new ValidatorException($"Liczba wierszy minimum jest większa od maksimum");

            // Sprawdzenie kolumn
            if (MaxColumns.HasValue && !MinColumns.HasValue)
                if (MaxColumns.Value < 0)
                    throw new ValidatorException($"Liczba maksimum kolumn musi być >= 0");

            if (!MaxColumns.HasValue && MinColumns.HasValue)
                if (MinColumns.Value < 0)
                    throw new ValidatorException($"Liczba minimum kolumn musi być >= 0");

            if (MaxColumns.HasValue && MinColumns.HasValue)
                if (MinColumns.Value > MaxColumns.Value)
                    throw new ValidatorException($"Liczba kolumn minimum jest większa od maksimum");

            // Sprawdź typ elementów
            if (this.Type == null)
                throw new ValidatorException("Brak informacji o typie danych macierzy");

            this.Type.ValidateSchemaInformation();

            // sprawdź wartość domyślną
            this.Type.ValidateValue(this.DefaultValue);
        }

        public override void ValidateValue(object value)
        {
            // Sprawdzanie wartosci null
            if (this.Nullable && value == null)
                return; // jest null, dopuszczony

            if (!this.Nullable && value == null)
                throw new ValidatorException("Wartość null jest niedopuszczalna");

            if (!(value is JObject))
                throw new ValidatorException($"Wartość pola typu 'matrix' musi być obiektem (JObject) a jest {value.GetType().FullName}");

            JObject jobject = value as JObject;

            // sprawdź wiersze i kolumny
            int? rows = jobject["rows"]?.Value<int>();
            int? columns = jobject["columns"]?.Value<int>();

            if (rows == null || columns == null)
                throw new ValidatorException("Wartość macierzy musi posiadać liczbę wierszy 'rows' oraz liczbę kolumn 'columns");


            if (this.MinRows.HasValue && rows.Value < this.MinRows.Value)
                throw new ValidatorException("Liczba wierszy jest zbyt mała");
            if (this.MaxRows.HasValue && rows.Value > this.MaxRows.Value)
                throw new ValidatorException("Liczba wierszy jest zbyt duża");

            if (this.MinColumns.HasValue && columns.Value < this.MinColumns.Value)
                throw new ValidatorException("Liczba kolumn jest zbyt mała");
            if (this.MaxColumns.HasValue && columns.Value > this.MaxColumns.Value)
                throw new ValidatorException("Liczba kolumn jest zbyt duża");

            // sprwadzenie danych
            JArray jrows = jobject["data"]?.Value<JArray>();
            if (jrows == null)
                throw new ValidatorException("Brak pola 'data' z danymi (wierszami)");

            if (jrows.Count > rows.Value)
                throw new ValidatorException("Liczba wierszy w polu 'data' jest zbyt duża względem wartości pola 'rows'");

            for (int irow = 0; irow < jrows.Count; irow++)
            {
                JArray jrow = jrows[irow].Value<JArray>();
                if (jrow == null)
                    throw new ValidatorException($"W wierszu #{irow} spodziewano się listy wartości");

                if (jrow.Count > columns.Value)
                    throw new ValidatorException($"Liczba kolumn w wierszu #{irow} w polu 'data' jest zbyt duża względem wartości pola 'columns'");


                // Sprawdź wartości w wierszu
                foreach (JValue jvalue in jrow)
                    this.Type.ValidateValue(jvalue.Value);
            }
        }

    }
}