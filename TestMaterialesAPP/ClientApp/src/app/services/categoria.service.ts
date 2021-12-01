import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CategoriaService {
 
  constructor(private http: HttpClient) { }
  
  getAll(): Observable<any> { 
    return this.http.get(`${environment.baseUrl}/Categoria`);
  }
}
