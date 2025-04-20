import { Injectable } from '@angular/core';
import { environment } from '../../../../../environments/environment';
import { PersonResponseModel } from '../models/person-response.model';
import { HttpClient } from '@angular/common/http';
import { map, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PersonService {

  constructor(
    private http: HttpClient
  ) { }

  getPeople(): Observable<PersonResponseModel[]> {
    return this.http
      .get<any>(`${environment.apiUrl}person`)
      .pipe(map(response => response.data));
  }

  getPersonById(id: number): Observable<PersonResponseModel> {
    return this.http.get<PersonResponseModel>(`${environment.apiUrl}person/${id}`);
  }

  createPerson(person: PersonResponseModel): Observable<PersonResponseModel> {
    return this.http.post<PersonResponseModel>(`${environment.apiUrl}person`, person);
  }

  updatePerson(id: number, person: PersonResponseModel): Observable<PersonResponseModel> {
    return this.http.put<PersonResponseModel>(`${environment.apiUrl}person/${id}`, person);
  }

  deletePerson(id: number): Observable<void> {
    return this.http.delete<void>(`${environment.apiUrl}person/${id}`);
  }
}
