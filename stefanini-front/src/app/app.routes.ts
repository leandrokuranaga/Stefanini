import { Routes } from '@angular/router';
import { PersonComponent } from './main/pages/person/person.component';
import { HomeComponent } from './main/pages/home/home.component';
import { CitiesComponent } from './main/pages/cities/cities.component';

export const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'person', component: PersonComponent },
  { path: 'city', component: CitiesComponent },
];
