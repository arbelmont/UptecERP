import { UnidadeMedida } from "app/shared/enums/unidadeMedida";
import { StatusLote } from "app/shared/enums/statusLote";

export class LoteHelper {

    public static getStatusClass(status: number): string {
        let classe = 'badge badge-pill text-white ';
        if (status == StatusLote.Aberto)
            classe += 'badge-success';
        else if (status == StatusLote.Fechado)
            classe += 'badge-danger';
        return classe;
    }

    public static getDescricaoStatus(status: string): string {
        return StatusLote[status];
    }

    public static getDescricaoUnidadeMedida(tipo: string) {
        return UnidadeMedida[tipo];
    }
}