import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Autor } from '../models/autor.model';

@Injectable({
  providedIn: 'root'
})
export class AutorService {
  private baseUrl = 'https://localhost:7217/api/Autor';

  constructor(private httpClient: HttpClient) {  }

  findAll(): Observable<Autor[]> {
    // variavel de escopo de bloco

    return this.httpClient.get<Autor[]>(`${this.baseUrl}`);
  }

  findById(id: string): Observable<Autor> {
    console.log(id)
    return this.httpClient.get<Autor>(`${this.baseUrl}/${id}`);
  }

  insert(autor: Autor): Observable<Autor> {
    
    const obj = {
      idAutortulo: autor.idAutor,
      nome : autor.nome
    }

    return this.httpClient.post<Autor>(this.baseUrl, obj);
  }
  
  update(autor: Autor): Observable<Autor> {
    const obj = {
        idAutor: autor.idAutor,
        nome : autor.nome
    }
    return this.httpClient.put<Autor>(`${this.baseUrl}/${autor.idAutor}`, obj);
  }

  delete(id: number): Observable<void> { // Alteração aqui
    return this.httpClient.delete<void>(`${this.baseUrl}/${id}`);
  }

}