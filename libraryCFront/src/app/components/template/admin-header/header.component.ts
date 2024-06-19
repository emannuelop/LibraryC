import { Component, HostListener, OnDestroy, OnInit } from '@angular/core';
import { MatIcon } from '@angular/material/icon';
import { MatToolbar } from '@angular/material/toolbar';
import { MatBadge } from '@angular/material/badge';
import { Usuario } from '../../../models/usuario.model';
import { AuthService } from '../../../services/auth.service';
import { LocalStorageService } from '../../../services/local-storage.service';
import { SidebarService } from '../../../services/sidebar.service';
import { Subscription } from 'rxjs';
import { MatButton, MatIconButton } from '@angular/material/button';
import { Router, RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { UsuarioService } from '../../../services/usuario.service';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [MatToolbar, MatIcon, MatBadge, MatButton, MatIconButton, RouterModule, CommonModule, FormsModule],
  templateUrl: './header.component.html',
  styleUrl: './header.component.css'
})
export class HeaderComponent implements OnInit, OnDestroy {
  usuarioLogado: Usuario | null = null;
  private subscription = new Subscription();
  showLoginOptions: boolean = false;
  showCart: boolean = false;
  qtdItensCart: number = 0;
  searchQuery: string = '';
  genericImage = 'assets/images/default-profile.jpg';
  logo = 'assets/images/logo.png';
  cartItems: any[] = [];
  totalPrice: number = 0;

  constructor(
    private sidebarService: SidebarService,
    private authService: AuthService,
    private usuarioService: UsuarioService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.getUsuarioLogado();
  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
  }

  onSubmit() {
    this.searchQuery = this.searchQuery.trim()
    if (this.searchQuery) {
      this.router.navigate(['/search/name'], { queryParams: { q: this.searchQuery.trim() } });
    }
  }

  clearSearch(): void {
    this.searchQuery = '';
  }

  calculateTotalPrice() {
    this.totalPrice = this.cartItems.reduce((total, item) => total + item.quantity * item.price, 0);
  }

  getUsuarioLogado() {
    this.subscription.add(this.authService.getUsuarioLogado().subscribe(
      usuario => this.usuarioLogado = usuario
    ));
  }


  deslogar() {
    this.authService.removeToken();
    this.authService.removeUsuarioLogado();
    this.router.navigate(['/login']);
  }

  alterarSenha() {
    this.router.navigate(['/admin/alterar-senha']);
  }

  dashboard() {
    this.router.navigate(['/admin/profile']);
  }

  clickMenu() {
    this.sidebarService.toggle();
  }

  goToCart() {
    this.router.navigate(['/cart']);
    this.showCart = false;
  }

  toggleCart() {
    this.showCart = !this.showCart;
  }

  @HostListener('document:click', ['$event'])
  onClickOutside(event: MouseEvent) {
    const target = event.target as HTMLElement;
    if (target && !target.closest('.cart-dropdown')) {
      this.showCart = false;
    }
  }
}