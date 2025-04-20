import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CityListComponent } from "./city-list/city-list.component";

@Component({
  selector: 'app-cities',
  imports: [CommonModule, CityListComponent],
  templateUrl: './cities.component.html',
  styleUrl: './cities.component.css',
})
export class CitiesComponent {




}
