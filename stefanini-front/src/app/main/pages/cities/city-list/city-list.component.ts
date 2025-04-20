import { Component, inject } from '@angular/core';
import { CityService } from '../services/city.service';
import { CityResponseModel } from '../models/city-response.model';
import { Observable } from 'rxjs';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-city-list',
  imports: [CommonModule],
  templateUrl: './city-list.component.html',
  styleUrl: './city-list.component.css'
})
export class CityListComponent {
  public cities: CityResponseModel[] = [];

  city$!: Observable<CityResponseModel[]>;
  private readonly cityService = inject(CityService);

  constructor() {
    this.city$ = this.cityService.getCities();
    console.log(this.city$);
  }
}
