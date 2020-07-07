import { TipoEmissor } from "app/shared/enums/tipoEmissor";
import { UnidadeMedida } from "app/shared/enums/unidadeMedida";
import { StatusNotaEntradaItem } from "app/shared/enums/statusNotaEntradaItem";

export class NotaEntradaItemHelper {

    public static getStatusClass(status: number): string {
        let classe = 'badge badge-pill text-white ';
        if (status == StatusNotaEntradaItem.Conciliar)
            classe += 'badge-warning';
        else if (status == StatusNotaEntradaItem.Recebida)
            classe += 'badge-success';
        else if (status == StatusNotaEntradaItem["A Cobrir"])
            classe += 'badge-info';
        return classe;
    }

    public static getDescricaoStatus(status: string): string {
        return StatusNotaEntradaItem[status];
    }

    public static getDescricaoTipoEmissor(tipo: string) {
        return TipoEmissor[tipo];
    }

    public static getDescricaoUnidadeMedida(tipo: string) {
        return UnidadeMedida[tipo];
    }
}