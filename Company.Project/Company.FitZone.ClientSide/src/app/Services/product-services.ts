  import { Injectable } from '@angular/core';
  import { IProduct } from '../Models/iproduct';
  import { Observable } from 'rxjs';
  import { HttpClient } from '@angular/common/http';

  @Injectable({
    providedIn: 'root'
  })
  export class ProductServices {

    private apiUrl = 'http://127.0.0.1:5297/api/Products/all';

    constructor(private http: HttpClient) {}

    getAllProducts(): Observable<IProduct[]> {
      
      return this.http.get<IProduct[]>(this.apiUrl);
    }
    
  }
