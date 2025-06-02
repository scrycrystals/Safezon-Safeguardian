using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using app2.Models;
using app2.Services;
using app2.Database;
using Dapper;
using MySql.Data.MySqlClient;
using Xamarin.Essentials;


namespace app2.ViewModels
{
    public partial class AddRiskyLocationViewModel : ObservableObject
    {
        private readonly ILocationService _locationService;
        private List<LocationResult> _allResults = new();
        private int _currentCount = 5;

        [ObservableProperty]
        string searchQuery;

        [ObservableProperty]
        ObservableCollection<LocationResult> locationResults = new();

        [ObservableProperty]
        LocationResult selectedLocation;

        [ObservableProperty]
        bool isLoadMoreVisible;

        [ObservableProperty]
        double currentLatitude;

        [ObservableProperty]
        double currentLongitude;


        public AddRiskyLocationViewModel(ILocationService locationService)
        {
            _locationService = locationService;
        }

        [RelayCommand]
        public async Task SearchLocationAsync()
        {
            await Shell.Current.DisplayAlert("Debug", "SearchLocationAsync called", "OK");

            if (string.IsNullOrWhiteSpace(SearchQuery))
                return;
            string modifiedQuery = $"{SearchQuery} Pakistan";

            _allResults = await _locationService.SearchLocationsAsync(modifiedQuery);

            LocationResults.Clear();  // 🔥 Clear previous search results

            if (_allResults == null || _allResults.Count == 0)
            {
                await Shell.Current.DisplayAlert("No Results", "No locations found. Try again.", "OK");
                return;
            }

            _currentCount = 0; // 🔥 Reset count whenever user searches
            LoadInitialLocations();
        }


        private void LoadInitialLocations()
        {
            foreach (var loc in _allResults.Take(5))
                LocationResults.Add(loc);

            _currentCount = LocationResults.Count;
        }


        [RelayCommand]
        public void SelectLocation(LocationResult location)
        {
            SelectedLocation = location; // Update the selected location
        }


        [RelayCommand]
        public async Task LoadMore()
        {
            var nextItems = _allResults.Skip(_currentCount).Take(5).ToList();

            if (nextItems == null || nextItems.Count == 0)
            {
                await Shell.Current.DisplayAlert("Info", "No more results to load!", "OK");
                return;
            }

            foreach (var loc in nextItems)
                LocationResults.Add(loc);

            _currentCount += nextItems.Count;
        }




        [RelayCommand]
        private async Task AddLocationToDatabaseAsync()
        {
            if (SelectedLocation == null)
                return;

            string locationName = SelectedLocation.DisplayName;
            int commaIndex = locationName.IndexOf(',');

            if (commaIndex != -1)
            {
                // If there's a comma, take the substring before it
                locationName = locationName.Substring(0, commaIndex);
                Console.WriteLine(locationName);
            }

            int userId = UserSession.UserId;
            if (userId == 0) return;

            using (var connection = DatabaseConnection.GetConnection())
            {
                if (connection.State != System.Data.ConnectionState.Open)
                    await connection.OpenAsync();

                string query = @"
                    INSERT INTO RiskyLocations (UserId, Latitude, Longitude, LocationName)
                    VALUES (@UserId, @Latitude, @Longitude, @LocationName)";

                await connection.ExecuteAsync(query, new
                {
                    UserId = userId,
                    Latitude = SelectedLocation.Latitude,
                    Longitude = SelectedLocation.Longitude,
                    LocationName = locationName // ✅ use the shortened name
                });


                MessagingCenter.Send(this, "LocationAdded");
            }
        }

        [RelayCommand]
        private async Task AddManualLocationAsync()
        {
            string locationName = await Shell.Current.DisplayPromptAsync("Manual Location", "Enter location name:");
            if (string.IsNullOrWhiteSpace(locationName))
                return;

            string latitudeString = await Shell.Current.DisplayPromptAsync("Manual Location", "Enter latitude:");
            if (string.IsNullOrWhiteSpace(latitudeString))
                return;

            string longitudeString = await Shell.Current.DisplayPromptAsync("Manual Location", "Enter longitude:");
            if (string.IsNullOrWhiteSpace(longitudeString))
                return;

            if (!double.TryParse(latitudeString, out double latitude) ||
                !double.TryParse(longitudeString, out double longitude))
            {
                await Shell.Current.DisplayAlert("Invalid Input", "Latitude and Longitude must be valid numbers.", "OK");
                return;
            }

            int userId = UserSession.UserId;
            if (userId == 0)
                return;

            using (var connection = DatabaseConnection.GetConnection())
            {
                if (connection.State != System.Data.ConnectionState.Open)
                    await connection.OpenAsync();

                string query = @"
            INSERT INTO RiskyLocations (UserId, Latitude, Longitude, LocationName)
            VALUES (@UserId, @Latitude, @Longitude, @LocationName)";

                await connection.ExecuteAsync(query, new
                {
                    UserId = userId,
                    Latitude = latitude,
                    Longitude = longitude,
                    LocationName = locationName
                });



                MessagingCenter.Send(this, "LocationAdded");
                await Shell.Current.DisplayAlert("Success", "Manual location added successfully!", "OK");
                
            }
        }
    }
}
