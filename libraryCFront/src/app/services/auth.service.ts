import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Usuario } from '../models/usuario.model';
import { LocalStorageService } from './local-storage.service';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private baseURL: string = 'https://localhost:7217/api/Auth';
  private tokenKey = 'jwt_token';
  private usuarioLogadoKey = 'usuario_logado';

  private usuarioLogadoSubject = new BehaviorSubject<Usuario | null>(null);
  Usuario$ = this.usuarioLogadoSubject.asObservable();

  constructor(
              private http: HttpClient, 
              private localStorageService: LocalStorageService, 
              private jwtHelper: JwtHelperService
            ){
    this.initUsuarioLogado();
  }
  

  private initUsuarioLogado() {
    const Usuario = localStorage.getItem(this.usuarioLogadoKey);
    if (Usuario) {
      const usuarioLogado = JSON.parse(Usuario);

      this.setUsuarioLogado(usuarioLogado);
      this.usuarioLogadoSubject.next(usuarioLogado);
    }
  }

  login(login: string, password: string): Observable<any> {
    const params = {
      email: login,
      password: password
    }

    const sla = this.http.post<string>(`${this.baseURL}/login`, params);


    sla.subscribe(data => {
      console.log(data);
      this.setToken(data)
    });

    console.log(this.getToken());

    //{ observe: 'response' } para garantir que a resposta completa seja retornada (incluindo o cabeçalho)
    return this.http.post(`${this.baseURL}/login`, params, {observe: 'response'}).pipe(
      tap((res: any) => {
        const authToken = res.headers.get('Authorization') ?? '';
        console.log(res.headers.get('Authorization') ?? '');
        if (authToken) {
          const usuarioLogado = res.body;
          console.log(usuarioLogado);
          if (usuarioLogado) {
            this.setUsuarioLogado(usuarioLogado);
            this.usuarioLogadoSubject.next(usuarioLogado);
          }
        }
      })
    );
  }

  setUsuarioLogado(Usuario: Usuario): void {
    this.localStorageService.setItem(this.usuarioLogadoKey, Usuario);
  }

  setToken(token: string): void {
    this.localStorageService.setItem(this.tokenKey, token);
  }

  getUsuarioLogado() {
    return this.usuarioLogadoSubject.asObservable();
  }

  getToken(): string | null {
    return this.localStorageService.getItem(this.tokenKey);
  }

  removeToken(): void {
    this.localStorageService.removeItem(this.tokenKey);
  }

  removeUsuarioLogado(): void {
    this.localStorageService.removeItem(this.usuarioLogadoKey);
    this.usuarioLogadoSubject.next(null);
  }

  isTokenExpired(): boolean {
    const token = this.getToken();
    // Verifica se o token é nulo ou está expirado
    return !token || this.jwtHelper.isTokenExpired(token);
    // npm install @auth0/angular-jwt
  }

  isLoggedIn(): boolean {
    const Usuario = JSON.parse(localStorage.getItem('usuario_logado') || '{}');
    return !!Usuario && !!Usuario.token;  // Verifica se há um token de autenticação no objeto Usuario
  }

}