import { Component, OnInit, Inject} from '@angular/core';
import { Autor } from '../../../models/autor.model';
import { AutorService } from '../../../services/autor.service';
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
  selector: 'app-autor-list',
  standalone: true,
  imports: [NgFor, MatTableModule, MatToolbarModule, MatIconModule
    , MatButtonModule, RouterModule, MatPaginatorModule, CommonModule],
  templateUrl: './autor-list.component.html',
  styleUrl: './autor-list.component.css'
})
export class AutorListComponent implements OnInit {
  displayedColumns: string[] = ['idAutor', 'nome'];
  autores: Autor[] = [];

  // variaveis de controle de paginacao
  totalRecords = 0;
  pageSize = 8;
  page = 0;

  constructor(private autorService: AutorService,
              public dialog: MatDialog
            ) {}

  ngOnInit(): void {
    this.loadAutores()
  }

  loadAutores(): void {
    this.autorService.findAll().subscribe(data => {
      this.autores = data;
      console.log(this.autores);
    });
  }
  
  // Método para paginar os resultados
  paginar(event: PageEvent): void {
    this.page = event.pageIndex;
    this.pageSize = event.pageSize;
    this.loadAutores();
  }

  delete(id: number): void {
    if (confirm('Tem certeza que deseja excluir este autores?')) {
      this.autorService.delete(id).subscribe(() => {
        this.loadAutores();
      });
    }
  }

}
