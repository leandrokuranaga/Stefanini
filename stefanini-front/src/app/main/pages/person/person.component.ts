import { Component } from '@angular/core';
import { PersonListComponent } from "./person-list/person-list.component";

@Component({
  selector: 'app-person',
  imports: [PersonListComponent],
  templateUrl: './person.component.html',
  styleUrl: './person.component.css',
  standalone: true,
})
export class PersonComponent {

}
