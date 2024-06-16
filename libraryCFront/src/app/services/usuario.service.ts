import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Usuario } from '../models/usuario.model';

@Injectable({
  providedIn: 'root'
})
export class UsuarioService {
  private baseUrl = 'https://localhost:7217/api/Usuario';

  constructor(private httpClient: HttpClient) {  }

  findAll(): Observable<Usuario[]> {
    // variavel de escopo de bloco

    return this.httpClient.get<Usuario[]>(`${this.baseUrl}`);
  }

  findById(id: string): Observable<Usuario> {
    console.log(id)
    return this.httpClient.get<Usuario>(`${this.baseUrl}/${id}`);
  }

  insert(usuario: Usuario): Observable<Usuario> {
    
    const obj = {
      name: usuario.name,
      email : usuario.email,
      cpf: usuario.cpf,
      password: usuario.password
    }

    return this.httpClient.post<Usuario>(this.baseUrl, obj);
  }
  
  update(usuario: Usuario): Observable<Usuario> {
    const obj = {
        name: usuario.name,
        email : usuario.email,
        cpf: usuario.cpf,
        password: usuario.password
    }
    return this.httpClient.put<Usuario>(`${this.baseUrl}/${usuario.id}`, obj);
  }

  delete(id: number): Observable<void> { // Alteração aqui
    return this.httpClient.delete<void>(`${this.baseUrl}/${id}`);
  }

}