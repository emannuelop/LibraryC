import { ActivatedRouteSnapshot, ResolveFn, RouterStateSnapshot } from "@angular/router";
import { inject } from "@angular/core";
import { EmprestimoService } from "../../../services/emprestimo.service";
import { Emprestimo } from "../../../models/emprestimo.model";


export const emprestimoResolver: ResolveFn<Emprestimo> =
    (route: ActivatedRouteSnapshot, state: RouterStateSnapshot) => {
        return inject(EmprestimoService).findById(route.paramMap.get('id')!);
    }