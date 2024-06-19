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
import { Livro } from '../../../models/livro.model';
import { HttpErrorResponse } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import {MatCheckboxModule} from '@angular/material/checkbox';
import { MatIcon } from '@angular/material/icon';
import { MatSnackBar } from '@angular/material/snack-bar';
import { LivroService } from '../../../services/livro.service';

@Component({
  selector: 'app-livro-form',
  standalone: true,
  imports: [NgIf, ReactiveFormsModule, MatFormFieldModule,
    MatInputModule, MatButtonModule, MatCardModule, MatToolbarModule, 
    RouterModule, MatSelectModule, CommonModule, MatCheckboxModule, MatIcon],
  templateUrl: './livro-form.component.html',
  styleUrl: './livro-form.component.css'
})
export class LivroFormComponent implements OnInit {

  formGroup: FormGroup;

  fileName: string = '';
  selectedFile: File | null = null;
  imagePreview: string | ArrayBuffer | null = null;
  apiResponse: any = null;

  constructor(
              private formBuilder: FormBuilder,
              private livroService: LivroService,
              private router: Router,
              private activatedRoute: ActivatedRoute,
              private location: Location,
              private snackBar: MatSnackBar
            ) {

    const livro: Livro = this.activatedRoute.snapshot.data['livro'];

    this.formGroup = formBuilder.group({
      idLivro: [(livro && livro.idLivro) ? livro.idLivro : null],
      titulo: ['', Validators.required],
      anoPublicacao: [null],
      idAutor: [null],
      quantidade: [null]
    });
  }
  ngOnInit(): void {
    this.initializeForm();
  }

  initializeForm() {

    const livro: Livro = this.activatedRoute.snapshot.data['livro'];

    console.log();

    // selecionando as associações

    this.formGroup = this.formBuilder.group({
      idLivro: [(livro && livro.idLivro) ? livro.idLivro : null],
      titulo: [(livro && livro.titulo) ? livro.titulo : '', 
        Validators.compose([Validators.required, 
                            Validators.minLength(3)])],
      anoPublicacao: [(livro && livro.anoPublicacao) ? livro.anoPublicacao : null],
      idAutor: [(livro && livro.idAutor) ? livro.idAutor : null],
      quantidade: [(livro && livro.quantidade) ? livro.quantidade : null]
    });

  }

  salvar() {
    console.log(this.formGroup);
    // marca todos os campos do formulario como 'touched'
    if (this.formGroup.valid) {
      const livro = this.formGroup.value;
      console.log(livro);
      if (livro.idLivro == null) {
        this.livroService.insert(livro).subscribe({
          next: (livroCadastrado) => {
            console.log(livroCadastrado)
            this.showSnackbarTopPosition('Livro adicionado com sucesso!', 'Fechar');
            this.router.navigateByUrl('/admin/livros');
          },
          error: (errorResponse) => {      
            console.log('Erro ao incluir' + JSON.stringify(errorResponse));
          }
        });
      } else {
        this.livroService.update(livro).subscribe({
          next: (livroAtualizado) => {
            console.log(livroAtualizado.idLivro)
            this.showSnackbarTopPosition('Livro atualizado com sucesso!', 'Fechar');
            this.router.navigateByUrl('/admin/livros');
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
      const livro = this.formGroup.value;
      if (livro.id != null) {
        this.livroService.delete(livro.id).subscribe({
          next: () => {
            this.router.navigateByUrl('/admin/admin/livros');
            this.showSnackbarTopPosition('Livro deletado com sucesso!', 'Fechar');
          },
          error: (err) => {
            console.log('Erro ao Excluir' + JSON.stringify(err));
            this.showSnackbarTopPosition('Erro ao deletar o Livro!', 'Fechar');
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
