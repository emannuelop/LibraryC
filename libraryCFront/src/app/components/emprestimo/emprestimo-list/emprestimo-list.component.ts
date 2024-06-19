import { Component, OnInit, Inject} from '@angular/core';
import { Emprestimo } from '../../../models/emprestimo.model';
import { EmprestimoService } from '../../../services/emprestimo.service';
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
  selector: 'app-emprestimo-list',
  standalone: true,
  imports: [NgFor, MatTableModule, MatToolbarModule, MatIconModule
    , MatButtonModule, RouterModule, MatPaginatorModule, CommonModule],
  templateUrl: './emprestimo-list.component.html',
  styleUrl: './emprestimo-list.component.css'
})
export class EmprestimoListComponent implements OnInit {
  displayedColumns: string[] = ['idEmprestimo', 'idCliente', 'idLivro' , 'dataEmprestimo', 'dataPrevistaDevolucao', 'dataDevolucao', 'status', 'action'];
  emprestimos: Emprestimo[] = [];

  // variaveis de controle de paginacao
  totalRecords = 0;
  pageSize = 8;
  page = 0;

  constructor(private emprestimoService: EmprestimoService,
              public dialog: MatDialog
            ) {}

  ngOnInit(): void {
    this.loadEmprestimos()
  }

  loadEmprestimos(): void {
    this.emprestimoService.findAll().subscribe(data => {
      this.emprestimos = data;
      console.log(this.emprestimos);
    });
  }
  
  // Método para paginar os resultados
  paginar(event: PageEvent): void {
    this.page = event.pageIndex;
    this.pageSize = event.pageSize;
    this.loadEmprestimos();
  }

  delete(id: number): void {
    if (confirm('Tem certeza que deseja excluir este emprestimo?')) {
      this.emprestimoService.delete(id).subscribe((data) => {
        this.loadEmprestimos();
      });
    }
  }
  devolver(id: number): void {
    if (confirm('Tem certeza que foi devolvido este emprestimo?')) {
      this.emprestimoService.devolucao(id).subscribe((data) => {
        this.loadEmprestimos();
      });
    }
  }

}
