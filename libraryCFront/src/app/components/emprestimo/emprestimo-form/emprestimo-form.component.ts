import { NgIf, Location } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, ReactiveFormsModule, ValidationErrors, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import {MatCardModule} from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatToolbarModule } from '@angular/material/toolbar';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { Emprestimo } from '../../../models/emprestimo.model';
import { HttpErrorResponse } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import {MatCheckboxModule} from '@angular/material/checkbox';
import { MatIcon } from '@angular/material/icon';
import { MatSnackBar } from '@angular/material/snack-bar';
import { EmprestimoService } from '../../../services/emprestimo.service';

@Component({
  selector: 'app-emprestimo-form',
  standalone: true,
  imports: [NgIf, ReactiveFormsModule, MatFormFieldModule,
    MatInputModule, MatButtonModule, MatCardModule, MatToolbarModule, 
    RouterModule, MatSelectModule, CommonModule, MatCheckboxModule, MatIcon],
  templateUrl: './emprestimo-form.component.html',
  styleUrl: './emprestimo-form.component.css'
})
export class EmprestimoFormComponent implements OnInit {

  formGroup: FormGroup;

  fileName: string = '';
  selectedFile: File | null = null;
  imagePreview: string | ArrayBuffer | null = null;
  apiResponse: any = null;

  constructor(
              private formBuilder: FormBuilder,
              private emprestimoService: EmprestimoService,
              private router: Router,
              private activatedRoute: ActivatedRoute,
              private location: Location,
              private snackBar: MatSnackBar
            ) {

    const emprestimo: Emprestimo = this.activatedRoute.snapshot.data['emprestimo'];

    this.formGroup = formBuilder.group({
      idEmprestimo: [(emprestimo && emprestimo.idEmprestimo) ? emprestimo.idEmprestimo : null],
      idCliente: [null],
      idLivro: [null],
      dataPrevistaDevolucao: [null]
    });
  }
  ngOnInit(): void {
    this.initializeForm();
  }

  initializeForm() {

    const emprestimo: Emprestimo = this.activatedRoute.snapshot.data['emprestimo'];

    console.log();

    // selecionando as associações

    this.formGroup = this.formBuilder.group({
      idEmprestimo: [(emprestimo && emprestimo.idEmprestimo) ? emprestimo.idEmprestimo : null],
      idCliente: [(emprestimo && emprestimo.idCliente) ? emprestimo.idCliente : null],
      idLivro: [(emprestimo && emprestimo.idLivro) ? emprestimo.idLivro : null],
      dataPrevistaDevolucao: [(emprestimo && emprestimo.dataPrevistaDevolucao) ? emprestimo.dataPrevistaDevolucao : null]
    });

  }

  salvar() {
    console.log(this.formGroup);
    // marca todos os campos do formulario como 'touched'
    if (this.formGroup.valid) {
      const emprestimo = this.formGroup.value;
      console.log(emprestimo);
      if (emprestimo.idEmprestimo == null) {
        this.emprestimoService.insert(emprestimo).subscribe({
          next: (emprestimoCadastrado) => {
            console.log(emprestimoCadastrado)
            this.showSnackbarTopPosition('Emprestimo adicionado com sucesso!', 'Fechar');
            this.router.navigateByUrl('/emprestimos');
          },
          error: (errorResponse) => {      
            console.log('Erro ao incluir' + JSON.stringify(errorResponse));
          }
        });
      } else {
        this.emprestimoService.update(emprestimo).subscribe({
          next: (emprestimoAtualizado) => {
            console.log(emprestimoAtualizado.idEmprestimo)
            this.showSnackbarTopPosition('Emprestimo atualizado com sucesso!', 'Fechar');
            this.router.navigateByUrl('/emprestimos');
          },
          error: (err) => {
            console.log('Erro ao alterar' + JSON.stringify(err));
          }
        });        
      }
    }
  }

  tratarErros(error: HttpErrorResponse) {
    if (error.status === 400) {
      // erros relacionados a campos
      if (error.error?.errors) {
        error.error.errors.forEach((validationError: any) => {
          // obs: o fieldName tem o mesmo valor da api
          const formControl = this.formGroup.get(validationError.fieldName);
          console.log(validationError);
          if (formControl) {
            console.log(formControl);
            formControl.setErrors({ apiError: validationError.message });
          }
        });
      };
    } else if (error.status < 400) {
        // Erro genérico não relacionado a um campo específico.
        alert(error.error?.message || 'Erro genérico no envio do formulário.');
    } else if (error.status >= 500) {
        alert('Erro interno do servidor. Por favor, tente novamente mais tarde.');
    }
  }

  excluir() {
    if (this.formGroup.valid) {
      const emprestimo = this.formGroup.value;
      if (emprestimo.id != null) {
        this.emprestimoService.delete(emprestimo.id).subscribe({
          next: () => {
            this.router.navigateByUrl('/admin/emprestimos');
            this.showSnackbarTopPosition('Emprestimo deletado com sucesso!', 'Fechar');
          },
          error: (err) => {
            console.log('Erro ao Excluir' + JSON.stringify(err));
            this.showSnackbarTopPosition('Erro ao deletar o Emprestimo!', 'Fechar');
          }
        });
      }
    }
  }

  getErrorMessage(fieldName: string): string {
    const error = this.apiResponse.errors.find((error: any) => error.fieldName === fieldName);
    return error ? error.message : '';
  }

  voltarPagina() {
    this.location.back();
  }

  carregarImagemSelecionada(event: any) {
    this.selectedFile = event.target.files[0];

    if (this.selectedFile) {
      this.fileName = this.selectedFile.name;
      // carregando image preview
      const reader = new FileReader();
      reader.onload = e => this.imagePreview = reader.result;
      reader.readAsDataURL(this.selectedFile);
    }

  }

  showSnackbarTopPosition(content:any, action:any) {
    this.snackBar.open(content, action, {
      duration: 2000,
      verticalPosition: "top", // Allowed values are  'top' | 'bottom'
      horizontalPosition: "center" // Allowed values are 'start' | 'center' | 'end' | 'left' | 'right'
    });
  }
}
