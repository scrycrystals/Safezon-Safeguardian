using System.Text.Json.Serialization;

namespace app2.Models
{
    public class LocationResult
    {
        [JsonPropertyName("display_name")]
        public string DisplayName { get; set; }

        [JsonPropertyName("lat")]
        public string Latitude { get; set; }

        [JsonPropertyName("lon")]
        public string Longitude { get; set; }
    }
}
