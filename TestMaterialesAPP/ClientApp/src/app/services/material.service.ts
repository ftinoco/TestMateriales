import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class MaterialService {

  constructor(private http: HttpClient) { }

  getAll(): Observable<any> { 
    return this.http.get(`${environment.baseUrl}/Material`);
  }

  get(id): Observable<any> {
    return this.http.get(`${environment.baseUrl}/Material/${id}`);
  }

  create(data): Observable<any> {
    return this.http.post(`${environment.baseUrl}/Material`, data);
  }

  update(id, data): Observable<any> {
    return this.http.put(`${environment.baseUrl}/Material/${id}`, data);
  }

  delete(id): Observable<any> {
    return this.http.delete(`${environment.baseUrl}/Material/${id}`);
  } 
}
