using DonationApp.API.Hubs;
using DonationApp.Core.Interfaces;
using DonationApp.UseCase.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json.Linq;
using System.Text.Json;


namespace DonationApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        private IHubContext<MessageHub, IMessageHubClient> _messageHub;
        private readonly HttpClient _httpClient;
        public WeatherController(IHubContext<MessageHub, IMessageHubClient> messageHub, HttpClient httpClient)
        {
            _messageHub = messageHub;
            _httpClient=httpClient;
        }

        [HttpGet]
        public async Task<IActionResult> PushMessage()
        {
            var locations = new List<GeoLocation>
            {
                new GeoLocation(10.8333, 106.6667), // Ho Chi Minh City [10.8333, 106.6667]
                new GeoLocation(21.7, 104.8667), // Yen Bai  [21.7, 104.8667]
                new GeoLocation(21.2667, 106.2), // Bac Giang [21.2667, 106.2]
                new GeoLocation(21.5928, 105.8442), // Thai Nguyen [21.5928, 105.8442]
                new GeoLocation(21.1833, 106.05), // Bac Ninh [21.1833, 106.05]
            };

            foreach (var location in locations)
            {
                var weather = await GetWeather(location.Lat, location.Lon);
                if (weather != null)
                {
                    await _messageHub.Clients.All.PushNotificationAsync(weather);
                }

                await Task.Delay(1000);

            }

            return Ok();
        }

        private async Task<string> GetWeather(double lat, double lon)
        {
            string apiKey = "9ffd275e699722b3981fe6eda8765244";

            var url = $"https://api.openweathermap.org/data/2.5/weather?lat={lat}&lon={lon}&appid={apiKey}&units=metric";

            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                return "Not found";
            }

            var body = await response.Content.ReadAsStringAsync();

            var json = JObject.Parse(body);

            // Truy cập các trường trong JSON
            var cityName = json["name"]?.ToString();
            var temperature = json["main"]?["temp"]?.ToObject<double>();
            var humidity = json["main"]?["humidity"]?.ToObject<int>();
            var description = json["weather"]?[0]?["description"]?.ToString();
            var windSpeed = json["wind"]?["speed"]?.ToObject<double>();
            var rain = json["rain"]?["1h"]?.ToObject<double>();

            // Hiển thị kết quả
            Console.WriteLine($"City: {cityName}");
            Console.WriteLine($"Temperature: {temperature}°C");
            Console.WriteLine($"Humidity: {humidity}%");
            Console.WriteLine($"Description: {description}");

            string template = $"[Thời tiết] {cityName}, {description}, nhiệt độ {temperature}°C, độ ẩm {humidity}%, gió {windSpeed} m/s, lượng mưa {rain} mm/h";

            return template;
        }
    }


    public class GeoLocation
    {
        public double Lat { get; set; }
        public double Lon { get; set; }

        public GeoLocation(double lat, double lon)
        {
            Lat = lat;
            Lon = lon;
        }
    }

}
