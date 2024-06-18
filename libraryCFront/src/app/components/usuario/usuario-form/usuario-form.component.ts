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
import { Usuario } from '../../../models/usuario.model';
import { HttpErrorResponse } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import {MatCheckboxModule} from '@angular/material/checkbox';
import { MatIcon } from '@angular/material/icon';
import { MatSnackBar } from '@angular/material/snack-bar';
import { UsuarioService } from '../../../services/usuario.service';

@Component({
  selector: 'app-usuario-form',
  standalone: true,
  imports: [NgIf, ReactiveFormsModule, MatFormFieldModule,
    MatInputModule, MatButtonModule, MatCardModule, MatToolbarModule, 
    RouterModule, MatSelectModule, CommonModule, MatCheckboxModule, MatIcon],
  templateUrl: './usuario-form.component.html',
  styleUrl: './usuario-form.component.css'
})
export class UsuarioFormComponent implements OnInit {

  formGroup: FormGroup;

  fileName: string = '';
  selectedFile: File | null = null;
  imagePreview: string | ArrayBuffer | null = null;
  apiResponse: any = null;

  constructor(
              private formBuilder: FormBuilder,
              private usuarioService: UsuarioService,
              private router: Router,
              private activatedRoute: ActivatedRoute,
              private location: Location,
              private snackBar: MatSnackBar
            ) {

    const usuario: Usuario = this.activatedRoute.snapshot.data['usuario'];

    this.formGroup = formBuilder.group({
      idUsuario: [(usuario && usuario.id) ? usuario.id : null],
      nome: ['', Validators.required],
      email: ['', Validators.required],
      cpf: ['', Validators.required],
      perfil: ['', Validators.required],
      senha: ['', Validators.required]
    });
  }
  ngOnInit(): void {
    this.initializeForm();
  }

  initializeForm() {

    const usuario: Usuario = this.activatedRoute.snapshot.data['usuario'];

    // selecionando as associações

    this.formGroup = this.formBuilder.group({
      idUsuario: [(usuario && usuario.id) ? usuario.id : null],
      nome: [(usuario && usuario.nome) ? usuario.nome : '', 
        Validators.compose([Validators.required, 
                            Validators.minLength(3)])],
      email: [(usuario && usuario.email) ? usuario.email : '', 
        Validators.compose([Validators.required, 
                            Validators.minLength(3)])],
      perfil: [(usuario && usuario.perfil) ? usuario.perfil : '', 
        Validators.compose([Validators.required, 
                            Validators.minLength(3)])],
      cpf: [(usuario && usuario.cpf) ? usuario.cpf : '', 
        Validators.compose([Validators.required, 
                            Validators.minLength(3)])],
      senha: [(usuario && usuario.senha) ? usuario.senha : '', 
        Validators.compose([Validators.required, 
                            Validators.minLength(3)])]
    });

  }

  salvar() {
    console.log(this.formGroup);
    // marca todos os campos do formulario como 'touched'
    if (this.formGroup.valid) {
      const usuario = this.formGroup.value;
      console.log(usuario);
      if (usuario.id == null) {
        this.usuarioService.insert(usuario).subscribe({
          next: (usuarioCadastrado) => {
            console.log(usuarioCadastrado.id)
            this.showSnackbarTopPosition('Usuario adicionado com sucesso!', 'Fechar');
          },
          error: (errorResponse) => {      
            console.log('Erro ao incluir' + JSON.stringify(errorResponse));
          }
        });
      } else {
        this.usuarioService.update(usuario).subscribe({
          next: (usuarioAtualizado) => {
            console.log(usuarioAtualizado.id)
            this.showSnackbarTopPosition('Usuario atualizado com sucesso!', 'Fechar');
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
      const usuario = this.formGroup.value;
      if (usuario.id != null) {
        this.usuarioService.delete(usuario.id).subscribe({
          next: () => {
            this.router.navigateByUrl('/admin/usuarios');
            this.showSnackbarTopPosition('Usuario deletado com sucesso!', 'Fechar');
          },
          error: (err) => {
            console.log('Erro ao Excluir' + JSON.stringify(err));
            this.showSnackbarTopPosition('Erro ao deletar o Usuario!', 'Fechar');
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
