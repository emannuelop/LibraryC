import { Routes } from '@angular/router';
import { LoginComponent } from './components/login/login.component';
import { LivroListComponent } from './components/livro/livro-list/livro-list.component';
import { livroResolver } from './components/livro/resolver/livro-resolver';
import { LivroFormComponent } from './components/livro/livro-form/livro-form.component';
import { ClienteListComponent } from './components/cliente/cliente-list/cliente-list.component';

export const routes: Routes = [

    {
        path: '',
        title: 'livro',
        children: [
            { path: '', component: LoginComponent, title: 'Login' },
            { path: 'login', component: LoginComponent, title: 'Login'},
            { path: 'livros', component: LivroListComponent, title: 'Livro-List'},
            { path: 'livros/new', component: LivroFormComponent, title: 'Novo Livro'},
            { path: 'livros/edit/:id', component: LivroFormComponent, resolve: {livro: livroResolver}},
            { path: 'clientes', component: ClienteListComponent, title: 'Cliente-List'},
        ]
    }

];
