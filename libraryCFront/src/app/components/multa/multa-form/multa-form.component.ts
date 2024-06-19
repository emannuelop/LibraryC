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
import { Multa } from '../../../models/multa.model';
import { HttpErrorResponse } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import {MatCheckboxModule} from '@angular/material/checkbox';
import { MatIcon } from '@angular/material/icon';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MultaService } from '../../../services/multa.service';

@Component({
  selector: 'app-multa-form',
  standalone: true,
  imports: [NgIf, ReactiveFormsModule, MatFormFieldModule,
    MatInputModule, MatButtonModule, MatCardModule, MatToolbarModule, 
    RouterModule, MatSelectModule, CommonModule, MatCheckboxModule, MatIcon],
  templateUrl: './multa-form.component.html',
  styleUrl: './multa-form.component.css'
})
export class MultaFormComponent implements OnInit {

  formGroup: FormGroup;

  fileName: string = '';
  selectedFile: File | null = null;
  imagePreview: string | ArrayBuffer | null = null;
  apiResponse: any = null;
  listaString: String[]= [];

  constructor(
              private formBuilder: FormBuilder,
              private multaService: MultaService,
              private router: Router,
              private activatedRoute: ActivatedRoute,
              private location: Location,
              private snackBar: MatSnackBar
            ) {

    const multa: Multa = this.activatedRoute.snapshot.data['multa'];

    this.formGroup = formBuilder.group({
      idMulta: [(multa && multa.idMulta) ? multa.idMulta : null],
      idCliente: [null],
      valor: [null],
      data: [null],
      motivo: ['', Validators.required],
      status: ['', Validators.required]
    });
  }
  ngOnInit(): void {
    this.initializeForm();
    this.multaService.findAllStatus().subscribe(data => {
      this.listaString = data;
     this.initializeForm();
    });
  }

  initializeForm() {

    const multa: Multa = this.activatedRoute.snapshot.data['multa'];

    console.log();

    // selecionando as associações

    this.formGroup = this.formBuilder.group({
      idMulta: [(multa && multa.idMulta) ? multa.idMulta : null],
      idCliente: [(multa && multa.idCliente) ? multa.idCliente : null],
      valor: [(multa && multa.valor) ? multa.valor : null],
      motivo: [(multa && multa.motivo) ? multa.motivo : '', 
        Validators.compose([Validators.required, 
                            Validators.minLength(3)])],
      status: [(multa && multa.status) ? multa.status : '', 
        Validators.compose([Validators.required, 
                            Validators.minLength(3)])]
    });

  }

  salvar() {
    console.log(this.formGroup);
    // marca todos os campos do formulario como 'touched'
    if (this.formGroup.valid) {
      const multa = this.formGroup.value;
      console.log(multa);
      if (multa.idMulta == null) {
        this.multaService.insert(multa).subscribe({
          next: (multaCadastrado) => {
            console.log(multaCadastrado)
            this.showSnackbarTopPosition('Multa adicionado com sucesso!', 'Fechar');
            this.router.navigateByUrl('/admin/multas');
          },
          error: (errorResponse) => {      
            console.log('Erro ao incluir' + JSON.stringify(errorResponse));
          }
        });
      } else {
        this.multaService.update(multa).subscribe({
          next: (multaAtualizado) => {
            console.log(multaAtualizado.idMulta)
            this.showSnackbarTopPosition('Multa atualizado com sucesso!', 'Fechar');
            this.router.navigateByUrl('/admin/multas');
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
      const multa = this.formGroup.value;
      if (multa.id != null) {
        this.multaService.delete(multa.id).subscribe({
          next: () => {
            this.router.navigateByUrl('/admin/admin/multas');
            this.showSnackbarTopPosition('Multa deletado com sucesso!', 'Fechar');
          },
          error: (err) => {
            console.log('Erro ao Excluir' + JSON.stringify(err));
            this.showSnackbarTopPosition('Erro ao deletar o Multa!', 'Fechar');
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
