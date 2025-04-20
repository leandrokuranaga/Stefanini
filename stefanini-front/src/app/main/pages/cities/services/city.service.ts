import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../../../environments/environment';
import { map, Observable } from 'rxjs';
import { CityResponseModel } from '../models/city-response.model';


@Injectable({
  providedIn: 'root'
})
export class CityService {

  constructor(
    private http: HttpClient
  ) { }

  getCities(): Observable<CityResponseModel[]> {
    return this.http
      .get<any>(`${environment.apiUrl}city`)
      .pipe(map(response => response.data));
  }

  getCityById(id: number): Observable<CityResponseModel> {
    return this.http.get<CityResponseModel>(`${environment.apiUrl}city/${id}`);
  }

  createCity(city: CityResponseModel): Observable<CityResponseModel> {
    return this.http.post<CityResponseModel>(`${environment.apiUrl}city`, city);
  }

  updateCity(id: number, city: CityResponseModel): Observable<CityResponseModel> {
    return this.http.put<CityResponseModel>(`${environment.apiUrl}city/${id}`, city);
  }

  deleteCity(id: number): Observable<void> {
    return this.http.delete<void>(`${environment.apiUrl}city/${id}`);
  }
}
