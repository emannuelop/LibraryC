import { Component, OnInit, Inject} from '@angular/core';
import { Multa } from '../../../models/multa.model';
import { MultaService } from '../../../services/multa.service';
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
  selector: 'app-multa-list',
  standalone: true,
  imports: [NgFor, MatTableModule, MatToolbarModule, MatIconModule
    , MatButtonModule, RouterModule, MatPaginatorModule, CommonModule],
  templateUrl: './multa-list.component.html',
  styleUrl: './multa-list.component.css'
})
export class MultaListComponent implements OnInit {
  displayedColumns: string[] = ['idMulta', 'idCliente', 'valor' , 'data','motivo','status', 'action'];
  multas: Multa[] = [];

  // variaveis de controle de paginacao
  totalRecords = 0;
  pageSize = 8;
  page = 0;

  constructor(private multaService: MultaService,
              public dialog: MatDialog
            ) {}

  ngOnInit(): void {
    this.loadMultas()
  }

  loadMultas(): void {
    this.multaService.findAll().subscribe(data => {
      this.multas = data;
      console.log(this.multas);
    });
  }
  
  // Método para paginar os resultados
  paginar(event: PageEvent): void {
    this.page = event.pageIndex;
    this.pageSize = event.pageSize;
    this.loadMultas();
  }

  delete(id: number): void {
    if (confirm('Tem certeza que deseja excluir este multa?')) {
      this.multaService.delete(id).subscribe((data) => {
        this.loadMultas();
      });
    }
  }

}
