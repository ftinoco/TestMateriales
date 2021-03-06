import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class UnidadMedidaService {

  constructor(private http: HttpClient) { }
  
  getAll(): Observable<any> { 
    return this.http.get(`${environment.baseUrl}/UnidadMedida`);
  }
}
