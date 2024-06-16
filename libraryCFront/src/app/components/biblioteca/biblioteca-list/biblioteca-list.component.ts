import { Component, OnInit, Inject} from '@angular/core';
import { Biblioteca } from '../../../models/biblioteca.model';
import { BibliotecaService } from '../../../services/biblioteca.service';
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
  selector: 'app-biblioteca-list',
  standalone: true,
  imports: [NgFor, MatTableModule, MatToolbarModule, MatIconModule
    , MatButtonModule, RouterModule, MatPaginatorModule, CommonModule],
  templateUrl: './biblioteca-list.component.html',
  styleUrl: './biblioteca-list.component.css'
})
export class BibliotecaListComponent implements OnInit {
  displayedColumns: string[] = ['idBiblioteca', 'nome', 'endereco'];
  bibliotecas: Biblioteca[] = [];

  // variaveis de controle de paginacao
  totalRecords = 0;
  pageSize = 8;
  page = 0;

  constructor(private bibliotecaService: BibliotecaService,
              public dialog: MatDialog
            ) {}

  ngOnInit(): void {
    this.loadBibliotecas()
  }

  loadBibliotecas(): void {
    this.bibliotecaService.findAll().subscribe(data => {
      this.bibliotecas = data;
      console.log(this.bibliotecas);
    });
  }
  
  // Método para paginar os resultados
  paginar(event: PageEvent): void {
    this.page = event.pageIndex;
    this.pageSize = event.pageSize;
    this.loadBibliotecas();
  }

  delete(id: number): void {
    if (confirm('Tem certeza que deseja excluir este biblioteca?')) {
      this.bibliotecaService.delete(id).subscribe(() => {
        this.loadBibliotecas();
      });
    }
  }

}
