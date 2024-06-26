import { Routes } from '@angular/router';
import { LoginComponent } from './components/login/login.component';
import { LivroListComponent } from './components/livro/livro-list/livro-list.component';
import { livroResolver } from './components/livro/resolver/livro-resolver';
import { LivroFormComponent } from './components/livro/livro-form/livro-form.component';
import { ClienteListComponent } from './components/cliente/cliente-list/cliente-list.component';
import { AutorListComponent } from './components/autor/autor-list/autor-list.component';
import { UsuarioListComponent } from './components/usuario/usuario-list/usuario-list.component';
import { AutorFormComponent } from './components/autor/autor-form/autor-form.component';
import { autorResolver } from './components/autor/resolver/autor-resolver';
import { UsuarioFormComponent } from './components/usuario/usuario-form/usuario-form.component';
import { usuarioResolver } from './components/usuario/resolver/usuario-resolver';
import { ClienteFormComponent } from './components/cliente/cliente-form/cliente-form.component';
import { clienteResolver } from './components/cliente/resolver/cliente-resolver';
import { AdminTemplateComponent } from './components/template/admin-template/admin-template.component';
import { EmprestimoListComponent } from './components/emprestimo/emprestimo-list/emprestimo-list.component';
import { EmprestimoFormComponent } from './components/emprestimo/emprestimo-form/emprestimo-form.component';
import { emprestimoResolver } from './components/emprestimo/resolver/emprestimo-resolver';
import { MultaListComponent } from './components/multa/multa-list/multa-list.component';
import { MultaFormComponent } from './components/multa/multa-form/multa-form.component';
import { multaResolver } from './components/multa/resolver/multa-resolver';
import { UsuarioSenhaFormComponent } from './components/usuario/usuario-senha/usuariosenha-form.component';

export const routes: Routes = [

    {
        path: '',
        title: 'logi',
        children: [
            { path: '', component: LoginComponent, title: 'Login' },
            { path: 'login', component: LoginComponent, title: 'Login'}
            
        ]
        
    },
    {
        path: 'admin',
        component: AdminTemplateComponent,
        title: 'livro',
        children: [
            { path: '', component: LoginComponent, title: 'Login' },
            { path: 'login', component: LoginComponent, title: 'Login'},
            { path: 'livros', component: LivroListComponent, title: 'Livro-List'},
            { path: 'livros/new', component: LivroFormComponent, title: 'Novo Livro'},
            { path: 'livros/edit/:id', component: LivroFormComponent, resolve: {livro: livroResolver}},
            { path: 'clientes', component: ClienteListComponent, title: 'Cliente-List'},
            { path: 'clientes/new', component: ClienteFormComponent, title: 'Cliente-Form'},
            { path: 'clientes/edit/:id', component: ClienteFormComponent, resolve: {cliente: clienteResolver}},
            { path: 'autores', component: AutorListComponent, title: 'Autor-List'},
            { path: 'autores/new', component: AutorFormComponent, title: 'Nova Biblioteca'},
            { path: 'autores/edit/:id', component: AutorFormComponent, resolve: {autor: autorResolver}},
            { path: 'usuarios', component: UsuarioListComponent, title: 'Usuario-List'},
            { path: 'usuarios/new', component: UsuarioFormComponent, title: 'Usuario-Form'},
            { path: 'usuarios/edit/:id', component: UsuarioFormComponent, title: 'Usuario-Form', resolve: {usuario: usuarioResolver}},
            { path: 'emprestimos', component: EmprestimoListComponent, title: 'Emprestimo-List'},
            { path: 'emprestimos/new', component: EmprestimoFormComponent, title: 'Emprestimo-Form'},
            { path: 'emprestimos/edit/:id', component: EmprestimoFormComponent, resolve: {emprestimo: emprestimoResolver}},
            { path: 'multas', component: MultaListComponent, title: 'Multa-List'},
            { path: 'multas/new', component: MultaFormComponent, title: 'Multa-Form'},
            { path: 'multas/edit/:id', component: MultaFormComponent, resolve: {multa: multaResolver}},
            { path: 'alterar-senha', component: UsuarioSenhaFormComponent, title: 'Senha-Form'},
            
        ]
        
    }

];
