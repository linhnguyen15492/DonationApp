import { Component, OnInit } from '@angular/core';
import * as leaflet from 'leaflet';
import 'heatmap.js';
import 'leaflet.heat'
import { heatData } from "./locationdata";

declare const HeatmapOverlay: any;

@Component({
  selector: 'app-heatmap',
  imports: [],
  templateUrl: './heatmap.component.html',
  styleUrl: './heatmap.component.css'
})
export class HeatmapComponent implements OnInit {
  private map: any;

  ngOnInit(): void {
    this.initMap();
  }

  private initMap(): void {
    // Initialising map with center point by using the coordinates
    // Setting initial zoom to 3
    this.map = leaflet.map('map', {
      center: [ 13, 107 ],
      zoom: 5
    });

    // Initialising tiles to the map by using openstreetmap
    // Setting zoom levels
    const tiles = leaflet.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
      maxZoom: 18,
      minZoom: 6,
      attribution: '© <a href="http://www.openstreetmap.org/copyright">OpenStreetMap</a>'
    });

    // Adding tiles to the map
    tiles.addTo(this.map);

    // Setting up heat layer config
    const heatLayerConfig = {
      "radius": 0.5,
      "maxOpacity": .8,
      "scaleRadius": true,
      // property below is responsible for colorization of heat layer
      "useLocalExtrema": true,
      // here we need to assign property value which represent lat in our data
      latField: 'lat',
      // here we need to assign property value which represent lng in our data
      lngField: 'lng',
      // here we need to assign property value which represent valueField in our data
      valueField: 'count'
    };

    // Initialising heat layer and passing config
    const heatmapLayer = new HeatmapOverlay(heatLayerConfig);

    //Passing data to a layer
    heatmapLayer.setData(heatData);

    //Adding heat layer to a map
    heatmapLayer.addTo(this.map);
  }
}