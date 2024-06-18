import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Emprestimo } from '../models/emprestimo.model';

@Injectable({
  providedIn: 'root'
})
export class EmprestimoService {
  private baseUrl = 'https://localhost:7217/api/Emprestimo';

  constructor(private httpClient: HttpClient) {  }

  findAll(): Observable<Emprestimo[]> {
    // variavel de escopo de bloco

    return this.httpClient.get<Emprestimo[]>(`${this.baseUrl}`);
  }

  findById(id: string): Observable<Emprestimo> {
    console.log(id)
    return this.httpClient.get<Emprestimo>(`${this.baseUrl}/${id}`);
  }

  insert(emprestimo: Emprestimo): Observable<Emprestimo> {
    
    const obj = {
      idCliente: emprestimo.idCliente,
      idLivro : emprestimo.idLivro,
      dataPrevistaDevolucao: emprestimo.dataPrevistaDevolucao
    }

    return this.httpClient.post<Emprestimo>(this.baseUrl, obj);
  }
  
  update(emprestimo: Emprestimo): Observable<Emprestimo> {
    const obj = {
        idCliente: emprestimo.idCliente,
        idLivro : emprestimo.idLivro,
        dataPrevistaDevolucao: emprestimo.dataPrevistaDevolucao
      }
    return this.httpClient.put<Emprestimo>(`${this.baseUrl}/${emprestimo.idEmprestimo}`, obj);
  }

  delete(id: number): Observable<void> { // Alteração aqui
    return this.httpClient.delete<void>(`${this.baseUrl}/${id}`);
  }

}