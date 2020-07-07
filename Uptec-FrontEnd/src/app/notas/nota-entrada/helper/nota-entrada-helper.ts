import { StatusNotaEntrada } from "app/shared/enums/statusNotaEntrada";
import { TipoEmissor } from "app/shared/enums/tipoEmissor";
import { UnidadeMedida } from "app/shared/enums/unidadeMedida";

export class NotaEntradaHelper {

    public static getStatusClass(status: number): string {
        let classe = 'badge badge-pill text-white ';
        if (status == StatusNotaEntrada.Conciliar)
            classe += 'badge-warning';
        else if (status == StatusNotaEntrada.Recebida)
            classe += 'badge-success';
        else if (status == StatusNotaEntrada["A Cobrir"])
            classe += 'badge-info';
        return classe;
    }

    public static getDescricaoStatus(status: string): string {
        return StatusNotaEntrada[status];
    }

    public static getDescricaoTipoEmissor(tipo: string) {
        return TipoEmissor[tipo];
    }

    public static getDescricaoUnidadeMedida(tipo: string) {
        return UnidadeMedida[tipo];
    }
}