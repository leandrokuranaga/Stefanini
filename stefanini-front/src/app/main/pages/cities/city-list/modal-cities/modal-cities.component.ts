import { Component, EventEmitter, Output, inject } from '@angular/core';
import { Observable } from 'rxjs';
import { CommonModule } from '@angular/common';
import { CityResponseModel } from '../../models/city-response.model';
import { CityService } from '../../services/city.service';
import { DialogModule } from 'primeng/dialog';

@Component({
  selector: 'app-modal-cities',
  standalone: true,
  imports: [CommonModule, DialogModule],
  templateUrl: './modal-cities.component.html'
})
export class ModalCitiesComponent {
  @Output() onSelect = new EventEmitter<CityResponseModel>();
  cities$!: Observable<CityResponseModel[]>;
  isDialogVisible = false;

  private readonly cityService = inject(CityService);

  constructor() {
    this.cities$ = this.cityService.getCities();
    console.log('cities$', this.cities$);
  }


  selectCity(city: CityResponseModel) {
    this.onSelect.emit(city);
  }

  public LinkCity(cityId: number) {
  }
}
