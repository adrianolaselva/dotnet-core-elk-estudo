using System;
using System.ComponentModel.DataAnnotations;
using analyst_challenge.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace analyst_challenge.Models
{
    public class EventReceiver
    {
        public Guid Id { get; set; }
        [JsonConverter(typeof (UnixDateTimeConverter))]
        [Required(ErrorMessage = "Timestamp é obrigatório")]
        public DateTime Timestamp { get; set; }
        [Required(ErrorMessage = "Tag é obrigatório")]
        public string Tag { get; set; }
        [Required(ErrorMessage = "Valor é obrigatório")]
        public string Valor { get; set; }
        public EventStatus Status { get; set; }
    }
}
