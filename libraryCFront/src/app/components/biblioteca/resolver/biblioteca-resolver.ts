import { ActivatedRouteSnapshot, ResolveFn, RouterStateSnapshot } from "@angular/router";
import { inject } from "@angular/core";
import { BibliotecaService } from "../../../services/biblioteca.service";
import { Biblioteca } from "../../../models/biblioteca.model";


export const bibliotecaResolver: ResolveFn<Biblioteca> =
    (route: ActivatedRouteSnapshot, state: RouterStateSnapshot) => {
        return inject(BibliotecaService).findById(route.paramMap.get('id')!);
    }