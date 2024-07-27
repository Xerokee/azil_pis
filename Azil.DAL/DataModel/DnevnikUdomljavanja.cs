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
        public string ime_ljubimca { get; set; }
        public string tip_ljubimca { get; set; }
        public bool udomljen { get; set; }

        [JsonPropertyName("datum")]
        [JsonConverter(typeof(DateFormatConverter2))]
        public DateTime? datum { get; set; }

        [JsonPropertyName("vrijeme")]
        [JsonConverter(typeof(TimeFormatConverter))]
        public TimeSpan? vrijeme { get; set; }  // Dodano za vrijeme
        public string imgUrl { get; set; }
        public bool? stanje_zivotinje { get; set; }
        public bool? status_udomljavanja { get; set; }
    }

    public class DateFormatConverter2 : JsonConverter<DateTime?>
    {
        private readonly string _dateFormat2 = "yyyy-MM-dd";

        public override DateTime? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return DateTime.ParseExact(reader.GetString(), _dateFormat2, null);
        }

        public override void Write(Utf8JsonWriter writer, DateTime? value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value?.ToString(_dateFormat2));
        }
    }

    public class TimeFormatConverter : JsonConverter<TimeSpan?>
    {
        private readonly string _timeFormat = @"hh\:mm\:ss";

        public override TimeSpan? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return TimeSpan.ParseExact(reader.GetString(), _timeFormat, null);
        }

        public override void Write(Utf8JsonWriter writer, TimeSpan? value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value?.ToString(_timeFormat));
        }
    }
}