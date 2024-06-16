import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Biblioteca } from '../models/biblioteca.model';

@Injectable({
  providedIn: 'root'
})
export class BibliotecaService {
  private baseUrl = 'https://localhost:7217/api/Biblioteca';

  constructor(private httpClient: HttpClient) {  }

  findAll(): Observable<Biblioteca[]> {
    // variavel de escopo de bloco

    return this.httpClient.get<Biblioteca[]>(`${this.baseUrl}`);
  }

  findById(id: string): Observable<Biblioteca> {
    console.log(id)
    return this.httpClient.get<Biblioteca>(`${this.baseUrl}/${id}`);
  }

  insert(biblioteca: Biblioteca): Observable<Biblioteca> {
    
    const obj = {
      nome: biblioteca.nome,
      endereco : biblioteca.endereco
    }

    return this.httpClient.post<Biblioteca>(this.baseUrl, obj);
  }
  
  update(biblioteca: Biblioteca): Observable<Biblioteca> {
    const obj = {
      nome: biblioteca.nome,
      endereco : biblioteca.endereco
    }
    return this.httpClient.put<Biblioteca>(`${this.baseUrl}/${biblioteca.idBiblioteca}`, obj);
  }

  delete(id: number): Observable<void> { // Alteração aqui
    return this.httpClient.delete<void>(`${this.baseUrl}/${id}`);
  }

}