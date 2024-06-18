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
import { Cliente } from '../../../models/cliente.model';
import { HttpErrorResponse } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import {MatCheckboxModule} from '@angular/material/checkbox';
import { MatIcon } from '@angular/material/icon';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ClienteService } from '../../../services/cliente.service';

@Component({
  selector: 'app-cliente-form',
  standalone: true,
  imports: [NgIf, ReactiveFormsModule, MatFormFieldModule,
    MatInputModule, MatButtonModule, MatCardModule, MatToolbarModule, 
    RouterModule, MatSelectModule, CommonModule, MatCheckboxModule, MatIcon],
  templateUrl: './cliente-form.component.html',
  styleUrl: './cliente-form.component.css'
})
export class ClienteFormComponent implements OnInit {

  formGroup: FormGroup;

  fileName: string = '';
  selectedFile: File | null = null;
  imagePreview: string | ArrayBuffer | null = null;
  apiResponse: any = null;

  constructor(
              private formBuilder: FormBuilder,
              private clienteService: ClienteService,
              private router: Router,
              private activatedRoute: ActivatedRoute,
              private location: Location,
              private snackBar: MatSnackBar
            ) {

    const cliente: Cliente = this.activatedRoute.snapshot.data['cliente'];

    this.formGroup = formBuilder.group({
      idCliente: [(cliente && cliente.idCliente) ? cliente.idCliente : null],
      nome: ['', Validators.required],
      email: ['', Validators.required],
      cpf: ['', Validators.required],
      telefone: ['', Validators.required]
    });
  }
  ngOnInit(): void {
    this.initializeForm();
  }

  initializeForm() {

    const cliente: Cliente = this.activatedRoute.snapshot.data['cliente'];

    // selecionando as associações

    this.formGroup = this.formBuilder.group({
      idCliente: [(cliente && cliente.idCliente) ? cliente.idCliente : null],
      nome: [(cliente && cliente.nome) ? cliente.nome : '', 
        Validators.compose([Validators.required, 
                            Validators.minLength(3)])],
      email: [(cliente && cliente.email) ? cliente.email : '', 
        Validators.compose([Validators.required, 
                            Validators.minLength(3)])],
      telefone: [(cliente && cliente.telefone) ? cliente.telefone : '', 
        Validators.compose([Validators.required, 
                            Validators.minLength(3)])],
      cpf: [(cliente && cliente.cpf) ? cliente.cpf : '', 
        Validators.compose([Validators.required, 
                            Validators.minLength(3)])]
    });

  }

  salvar() {
    console.log(this.formGroup);
    // marca todos os campos do formulario como 'touched'
    if (this.formGroup.valid) {
      const cliente = this.formGroup.value;
      console.log(cliente);
      if (cliente.id == null) {
        this.clienteService.insert(cliente).subscribe({
          next: (clienteCadastrado) => {
            console.log(clienteCadastrado.idCliente)
            this.showSnackbarTopPosition('Cliente adicionado com sucesso!', 'Fechar');
            this.router.navigateByUrl('/clientes');
          },
          error: (errorResponse) => {      
            console.log('Erro ao incluir' + JSON.stringify(errorResponse));
          }
        });
      } else {
        this.clienteService.update(cliente).subscribe({
          next: (clienteAtualizado) => {
            console.log(clienteAtualizado)
            this.showSnackbarTopPosition('Cliente atualizado com sucesso!', 'Fechar');
            this.router.navigateByUrl('/clientes');
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
      const cliente = this.formGroup.value;
      if (cliente.id != null) {
        this.clienteService.delete(cliente.id).subscribe({
          next: () => {
            this.router.navigateByUrl('/admin/clientes');
            this.showSnackbarTopPosition('Cliente deletado com sucesso!', 'Fechar');
          },
          error: (err) => {
            console.log('Erro ao Excluir' + JSON.stringify(err));
            this.showSnackbarTopPosition('Erro ao deletar o Cliente!', 'Fechar');
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
