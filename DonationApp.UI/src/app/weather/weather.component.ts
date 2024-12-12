import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-weather',
  imports: [],
  templateUrl: './weather.component.html',
  styleUrl: './weather.component.css',
})
export class WeatherComponent implements OnInit {
  lat: number = 0;
  lon: number = 0;
  API_key: string = '9ffd275e699722b3981fe6eda8765244';

  constructor(private httpClient: HttpClient) {}
  ngOnInit(): void {
    this.getWeather();
  }

  getWeather() {
    let url = `https://api.openweathermap.org/data/2.5/weather?lat=${this.lat}&lon=${this.lon}&appid=${this.API_key}`;
    this.httpClient.get(url).subscribe({
      next: (data: any) => {
        console.log(data);
      },
      error: (error) => {
        console.log(error);
      },
    });
  }
}
