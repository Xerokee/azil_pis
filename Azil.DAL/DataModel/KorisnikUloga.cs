using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Azil.DAL.DataModel
{
    public partial class KorisnikUloga
    {
        public int id_korisnika { get; set; }
        public int id_uloge { get; set; }

        [JsonPropertyName("datum_od")]
        [JsonConverter(typeof(DateFormatConverter))]
        public DateTime datum_od { get; set; }

        [JsonPropertyName("datum_do")]
        [JsonConverter(typeof(DateFormatConverter))]
        public DateTime datum_do { get; set; }
        public virtual Uloge Uloge { get; set; }
    }

    public class DateFormatConverter : JsonConverter<DateTime>
    {
        private readonly string _dateFormat = "yyyy-MM-dd";

        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return DateTime.ParseExact(reader.GetString(), _dateFormat, null);
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString(_dateFormat));
        }
    }
}
