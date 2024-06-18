import { Component, OnInit, Inject} from '@angular/core';
import { Livro } from '../../../models/livro.model';
import { LivroService } from '../../../services/livro.service';
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
  selector: 'app-livro-list',
  standalone: true,
  imports: [NgFor, MatTableModule, MatToolbarModule, MatIconModule
    , MatButtonModule, RouterModule, MatPaginatorModule, CommonModule],
  templateUrl: './livro-list.component.html',
  styleUrl: './livro-list.component.css'
})
export class LivroListComponent implements OnInit {
  displayedColumns: string[] = ['idLivro', 'titulo', 'anoPublicacao' , 'idAutor', 'action'];
  livros: Livro[] = [];

  // variaveis de controle de paginacao
  totalRecords = 0;
  pageSize = 8;
  page = 0;

  constructor(private livroService: LivroService,
              public dialog: MatDialog
            ) {}

  ngOnInit(): void {
    this.loadLivros()
  }

  loadLivros(): void {
    this.livroService.findAll().subscribe(data => {
      this.livros = data;
      console.log(this.livros);
    });
  }
  
  // Método para paginar os resultados
  paginar(event: PageEvent): void {
    this.page = event.pageIndex;
    this.pageSize = event.pageSize;
    this.loadLivros();
  }

  delete(id: number): void {
    if (confirm('Tem certeza que deseja excluir este livro?')) {
      this.livroService.delete(id).subscribe((data) => {
        this.loadLivros();
      });
    }
  }

}
