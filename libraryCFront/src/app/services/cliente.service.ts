import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Cliente } from '../models/cliente.model';

@Injectable({
  providedIn: 'root'
})
export class ClienteService {
  private baseUrl = 'https://localhost:7217/api/Cliente';

  constructor(private httpClient: HttpClient) {  }

  findAll(): Observable<Cliente[]> {
    // variavel de escopo de bloco

    return this.httpClient.get<Cliente[]>(`${this.baseUrl}`);
  }

  findById(id: string): Observable<Cliente> {
    console.log(id)
    return this.httpClient.get<Cliente>(`${this.baseUrl}/${id}`);
  }

  insert(cliente: Cliente): Observable<Cliente> {
    
    const obj = {
      nome: cliente.nome,
      email : cliente.email,
      telefone: cliente.telefone,
      cpf : cliente.cpf
    }

    return this.httpClient.post<Cliente>(this.baseUrl, obj);
  }
  
  update(cliente: Cliente): Observable<Cliente> {
    const obj = {
        nome: cliente.nome,
        email : cliente.email,
        telefone: cliente.telefone,
        cpf : cliente.cpf
      }
    return this.httpClient.put<Cliente>(`${this.baseUrl}/${cliente.idCliente}`, obj);
  }

  delete(id: number): Observable<void> { // Alteração aqui
    return this.httpClient.delete<void>(`${this.baseUrl}/${id}`);
  }

}