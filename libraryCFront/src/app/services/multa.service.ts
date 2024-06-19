import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Multa } from '../models/multa.model';

@Injectable({
  providedIn: 'root'
})
export class MultaService {
  private baseUrl = 'https://localhost:7217/api/Multa';

  constructor(private httpClient: HttpClient) {  }

  findAll(): Observable<Multa[]> {
    // variavel de escopo de bloco

    return this.httpClient.get<Multa[]>(`${this.baseUrl}`);
  }

  findAllStatus(): Observable<String[]> {
    // variavel de escopo de bloco

    return this.httpClient.get<String[]>(`${this.baseUrl}/status`);
  }

  findById(id: string): Observable<Multa> {
    console.log(id)
    return this.httpClient.get<Multa>(`${this.baseUrl}/${id}`);
  }

  insert(multa: Multa): Observable<Multa> {
    
    const obj = {
      idCliente: multa.idCliente,
      valor : multa.valor,
      data: multa.data,
      motivo: multa.motivo,
      status: multa.status
    }

    return this.httpClient.post<Multa>(this.baseUrl, obj);
  }
  
  update(multa: Multa): Observable<Multa> {
    const obj = {
        idCliente: multa.idCliente,
        valor : multa.valor,
        data: multa.data,
        motivo: multa.motivo,
        status: multa.status
      }
    return this.httpClient.put<Multa>(`${this.baseUrl}/${multa.idMulta}`, obj);
  }

  delete(id: number): Observable<any> { // Alteração aqui
    console.log(id);
    return this.httpClient.delete<any>(`${this.baseUrl}/${id}`);
  }

}