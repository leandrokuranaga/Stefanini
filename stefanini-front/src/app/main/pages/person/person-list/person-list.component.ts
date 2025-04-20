import { Component, inject } from '@angular/core';
import { PersonResponseModel } from '../models/person-response.model';
import { Observable } from 'rxjs';
import { PersonService } from '../services/person.service';
import { CommonModule } from '@angular/common';
import { ButtonModule } from 'primeng/button';
import { DialogModule } from 'primeng/dialog';
import { CityResponseModel } from '../../cities/models/city-response.model';
import { ModalCitiesComponent } from '../../cities/city-list/modal-cities/modal-cities.component';

@Component({
  selector: 'app-person-list',
  imports: [CommonModule, ButtonModule, ModalCitiesComponent, DialogModule],
  templateUrl: './person-list.component.html',
  styleUrl: './person-list.component.css',
  standalone: true,

})
export class PersonListComponent {
  person$!: Observable<PersonResponseModel[]>;
  public isLoading: boolean = true;
  public isError: boolean = false;
  private readonly personService = inject(PersonService);
  cityDialogVisible = false;
  selectedPersonId: number | null = null;

  constructor() {
    this.person$ = this.personService.getPeople();
  }

  public displayModal(id: number) {
    this.selectedPersonId = id;
    this.cityDialogVisible = true;
  }

  public closeModal() {
    this.cityDialogVisible = false;
    this.selectedPersonId = null;
  }

  public onCitySelected(city: CityResponseModel) {
    if (!this.selectedPersonId) return;

    const payload = {
      cityId: city.id
    };

    this.personService
      .updatePerson(this.selectedPersonId, payload as any)
      .subscribe(() => {
        this.closeModal();
        this.person$ = this.personService.getPeople();
      });
  }

}
