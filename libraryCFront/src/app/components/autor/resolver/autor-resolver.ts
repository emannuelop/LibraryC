import { ActivatedRouteSnapshot, ResolveFn, RouterStateSnapshot } from "@angular/router";
import { inject } from "@angular/core";
import { AutorService } from "../../../services/autor.service";
import { Autor } from "../../../models/autor.model";


export const autorResolver: ResolveFn<Autor> =
    (route: ActivatedRouteSnapshot, state: RouterStateSnapshot) => {
        return inject(AutorService).findById(route.paramMap.get('id')!);
    }