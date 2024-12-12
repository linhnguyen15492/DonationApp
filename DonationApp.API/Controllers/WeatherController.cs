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
        public async Task<IActionResult> GetWeather()
        {
            var url = "https://api.openweathermap.org/data/2.5/weather?lat=10.7758439&lon=106.7017555&appid=9ffd275e699722b3981fe6eda8765244&units=metric";

            var response = await _httpClient.GetAsync(url);

            

            if (!response.IsSuccessStatusCode)
            {
                return BadRequest();
            }

            var body = await response.Content.ReadAsStringAsync();

            var json = JObject.Parse(body);

            // Truy cập các trường trong JSON
            var cityName = json["name"]?.ToString();
            var temperature = json["main"]?["temp"]?.ToObject<double>();
            var humidity = json["main"]?["humidity"]?.ToObject<int>();
            var description = json["weather"]?[0]?["description"]?.ToString();

            // Hiển thị kết quả
            Console.WriteLine($"City: {cityName}");
            Console.WriteLine($"Temperature: {temperature}°C");
            Console.WriteLine($"Humidity: {humidity}%");
            Console.WriteLine($"Description: {description}");


            return Ok(json);
        }
    }
}
