import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { AuthService } from '../services/auth.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

  constructor(private authService: AuthService, private router: Router) { }

  canActivate(): boolean {
    if (this.authService.isLoggedIn()) {
      return true; // Se o usuário estiver logado, permita o acesso
    } else {
      this.router.navigate(['/login']); // Se não estiver logado, redirecione para a página de login
      return false;
    }
  }
}