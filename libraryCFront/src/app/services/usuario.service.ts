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

  findAllRoles(): Observable<String[]> {
    // variavel de escopo de bloco

    return this.httpClient.get<String[]>(`${this.baseUrl}/roles`);
  }

  findById(id: string): Observable<Usuario> {
    console.log(id)
    return this.httpClient.get<Usuario>(`${this.baseUrl}/${id}`);
  }

  insert(usuario: Usuario): Observable<Usuario> {
    
    const obj = {
      nome: usuario.nome,
      email : usuario.email,
      senha: usuario.senha,
      perfil: usuario.perfil,
      cpf: usuario.cpf
    }

    return this.httpClient.post<Usuario>(this.baseUrl, obj);
  }
  
  update(usuario: Usuario): Observable<Usuario> {
    const obj = {
        nome: usuario.nome,
        email : usuario.email,
        cpf: usuario.cpf,
        perfil: usuario.perfil
    }
    return this.httpClient.put<Usuario>(`${this.baseUrl}/${usuario.idUsuario}`, obj);
  }

  delete(id: number): Observable<void> { // Alteração aqui
    return this.httpClient.delete<void>(`${this.baseUrl}/${id}`);
  }

  alterarSenha(senhaAtual: string, senhaNova: string): Observable<void> { // Alteração aqui
    const obj = {
      senhaAtual: senhaAtual,
      senhaNova : senhaNova
  }
    return this.httpClient.patch<void>(`${this.baseUrl}/alterar-senha`, obj);
  }

}