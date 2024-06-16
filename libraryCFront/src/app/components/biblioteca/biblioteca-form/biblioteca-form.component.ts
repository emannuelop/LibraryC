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
import { Biblioteca } from '../../../models/biblioteca.model';
import { HttpErrorResponse } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import {MatCheckboxModule} from '@angular/material/checkbox';
import { MatIcon } from '@angular/material/icon';
import { MatSnackBar } from '@angular/material/snack-bar';
import { BibliotecaService } from '../../../services/biblioteca.service';

@Component({
  selector: 'app-biblioteca-form',
  standalone: true,
  imports: [NgIf, ReactiveFormsModule, MatFormFieldModule,
    MatInputModule, MatButtonModule, MatCardModule, MatToolbarModule, 
    RouterModule, MatSelectModule, CommonModule, MatCheckboxModule, MatIcon],
  templateUrl: './biblioteca-form.component.html',
  styleUrl: './biblioteca-form.component.css'
})
export class BibliotecaFormComponent implements OnInit {

  formGroup: FormGroup;

  fileName: string = '';
  selectedFile: File | null = null;
  imagePreview: string | ArrayBuffer | null = null;
  apiResponse: any = null;

  constructor(
              private formBuilder: FormBuilder,
              private bibliotecaService: BibliotecaService,
              private router: Router,
              private activatedRoute: ActivatedRoute,
              private location: Location,
              private snackBar: MatSnackBar
            ) {

    const biblioteca: Biblioteca = this.activatedRoute.snapshot.data['biblioteca'];

    this.formGroup = formBuilder.group({
      idBiblioteca: [(biblioteca && biblioteca.idBiblioteca) ? biblioteca.idBiblioteca : null],
      nome: ['', Validators.required],
      endereco: [null]
    });
  }
  ngOnInit(): void {
  }

  initializeForm() {

    const biblioteca: Biblioteca = this.activatedRoute.snapshot.data['biblioteca'];

    // selecionando as associações

    this.formGroup = this.formBuilder.group({
      idBiblioteca: [(biblioteca && biblioteca.idBiblioteca) ? biblioteca.idBiblioteca : null],
      titulo: [(biblioteca && biblioteca.nome) ? biblioteca.nome : '', 
        Validators.compose([Validators.required, 
                            Validators.minLength(3)])],
      endereco: [(biblioteca && biblioteca.endereco) ? biblioteca.endereco : null],
    });

  }

  salvar() {
    console.log(this.formGroup);
    // marca todos os campos do formulario como 'touched'
    if (this.formGroup.valid) {
      const biblioteca = this.formGroup.value;
      console.log(biblioteca);
      if (biblioteca.idBiblioteca == null) {
        this.bibliotecaService.insert(biblioteca).subscribe({
          next: (bibliotecaCadastrado) => {
            console.log(bibliotecaCadastrado.idBiblioteca)
            this.showSnackbarTopPosition('Biblioteca adicionado com sucesso!', 'Fechar');
          },
          error: (errorResponse) => {      
            console.log('Erro ao incluir' + JSON.stringify(errorResponse));
          }
        });
      } else {
        this.bibliotecaService.update(biblioteca).subscribe({
          next: (bibliotecaAtualizado) => {
            console.log(bibliotecaAtualizado.idBiblioteca)
            this.showSnackbarTopPosition('Biblioteca atualizado com sucesso!', 'Fechar');
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
      const biblioteca = this.formGroup.value;
      if (biblioteca.id != null) {
        this.bibliotecaService.delete(biblioteca.id).subscribe({
          next: () => {
            this.router.navigateByUrl('/admin/bibliotecas');
            this.showSnackbarTopPosition('Biblioteca deletado com sucesso!', 'Fechar');
          },
          error: (err) => {
            console.log('Erro ao Excluir' + JSON.stringify(err));
            this.showSnackbarTopPosition('Erro ao deletar o Biblioteca!', 'Fechar');
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
