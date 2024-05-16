using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Azil.DAL.DataModel
{
    public partial class DnevnikUdomljavanja
    {
        public int id_ljubimca { get; set; }
        public int id_korisnika { get; set; }

        [JsonPropertyName("datum")]
        [JsonConverter(typeof(DateFormatConverter2))]
        public DateTime datum { get; set; }
        public string opis { get; set; }
    }

    public class DateFormatConverter2 : JsonConverter<DateTime>
    {
        private readonly string _dateFormat2 = "yyyy-MM-dd";

        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return DateTime.ParseExact(reader.GetString(), _dateFormat2, null);
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString(_dateFormat2));
        }
    }
}