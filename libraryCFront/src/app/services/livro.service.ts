import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Livro } from '../models/livro.model';

@Injectable({
  providedIn: 'root'
})
export class LivroService {
  private baseUrl = 'https://localhost:7217/api/Livro';

  constructor(private httpClient: HttpClient) {  }

  findAll(): Observable<Livro[]> {
    // variavel de escopo de bloco

    return this.httpClient.get<Livro[]>(`${this.baseUrl}`);
  }

  findById(id: string): Observable<Livro> {
    console.log(id)
    return this.httpClient.get<Livro>(`${this.baseUrl}/${id}`);
  }

  insert(livro: Livro): Observable<Livro> {
    
    const obj = {
      titulo: livro.titulo,
      anoPublicacao : livro.anoPublicacao,
      idAutor: livro.idAutor
    }

    return this.httpClient.post<Livro>(this.baseUrl, obj);
  }
  
  update(livro: Livro): Observable<Livro> {
    const obj = {
      titulo: livro.titulo,
      anoPublicacao : livro.anoPublicacao,
      idAutor: livro.idAutor
    }
    return this.httpClient.put<Livro>(`${this.baseUrl}/${livro.idLivro}`, obj);
  }

  delete(id: number): Observable<any> { // Alteração aqui
    console.log(id);
    return this.httpClient.delete<any>(`${this.baseUrl}/${id}`);
  }

}