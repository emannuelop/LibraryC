import { Component, OnInit, Inject} from '@angular/core';
import { Usuario } from '../../../models/usuario.model';
import { UsuarioService } from '../../../services/usuario.service';
import { NgFor } from '@angular/common';
import { MatTableModule } from '@angular/material/table';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { RouterModule } from '@angular/router';
import { MatPaginatorModule, PageEvent } from '@angular/material/paginator';
import { CommonModule } from '@angular/common';

import {
  MatDialog,
  MatDialogActions,
  MatDialogClose,
  MatDialogTitle,
  MatDialogContent,
  MAT_DIALOG_DATA
} from '@angular/material/dialog';

@Component({
  selector: 'app-usuario-list',
  standalone: true,
  imports: [NgFor, MatTableModule, MatToolbarModule, MatIconModule
    , MatButtonModule, RouterModule, MatPaginatorModule, CommonModule],
  templateUrl: './usuario-list.component.html',
  styleUrl: './usuario-list.component.css'
})
export class UsuarioListComponent implements OnInit {
  displayedColumns: string[] = ['idUsuario', 'nome', 'email' , 'cpf', 'senha'];
  usuarios: Usuario[] = [];

  // variaveis de controle de paginacao
  totalRecords = 0;
  pageSize = 8;
  page = 0;

  constructor(private usuarioService: UsuarioService,
              public dialog: MatDialog
            ) {}

  ngOnInit(): void {
    this.loadUsuarios()
  }

  loadUsuarios(): void {
    this.usuarioService.findAll().subscribe(data => {
      this.usuarios = data;
      console.log(this.usuarios);
    });
  }
  
  // Método para paginar os resultados
  paginar(event: PageEvent): void {
    this.page = event.pageIndex;
    this.pageSize = event.pageSize;
    this.loadUsuarios();
  }

  delete(id: number): void {
    if (confirm('Tem certeza que deseja excluir este usuario?')) {
      this.usuarioService.delete(id).subscribe(() => {
        this.loadUsuarios();
      });
    }
  }

}
