using app2.Models;
using System.Text.Json;
using app2.Services;

namespace app2.Services
{
    public class LocationService : ILocationService
    {
        public async Task<List<LocationResult>> SearchLocationsAsync(string query)
        {
            var url = $"https://nominatim.openstreetmap.org/search?q={query}&format=json&addressdetails=1&accept-language=en";

            using var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("User-Agent", "Safezone/1.0");
            var response = await httpClient.GetStringAsync(url);
            Console.WriteLine($"API Response: {response}");

            var allLocations = JsonSerializer.Deserialize<List<LocationResult>>(response);

            if (allLocations == null)
                return new List<LocationResult>();

            var englishLocations = allLocations
                .Where(loc => !string.IsNullOrWhiteSpace(loc.DisplayName) && IsEnglish(loc.DisplayName))
                .ToList();

            return englishLocations;
        }

        private bool IsEnglish(string input)
        {
            return input.All(c => c <= 127);
        }
    }
}
