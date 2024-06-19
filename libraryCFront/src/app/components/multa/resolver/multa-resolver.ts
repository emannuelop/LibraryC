import { ActivatedRouteSnapshot, ResolveFn, RouterStateSnapshot } from "@angular/router";
import { inject } from "@angular/core";
import { MultaService } from "../../../services/multa.service";
import { Multa } from "../../../models/multa.model";


export const multaResolver: ResolveFn<Multa> =
    (route: ActivatedRouteSnapshot, state: RouterStateSnapshot) => {
        return inject(MultaService).findById(route.paramMap.get('id')!);
    }