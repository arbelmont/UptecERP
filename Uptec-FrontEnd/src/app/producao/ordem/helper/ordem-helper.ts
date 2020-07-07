import { OrdemMotivoExpedicaoDetalhe } from './../../../shared/enums/ordemMotivoExpedicaoDetalhe';
import { StatusOrdem } from "app/shared/enums/statusOrdem";

export class OrdemHelper {

    public static getStatusClass(status: number): string {
        let classe = 'badge badge-pill text-white ';
        if (status == StatusOrdem.Producao)
            classe += 'badge-warning';
        else if (status == StatusOrdem.Expedicao)
            classe += 'badge-info';
        else if (status == StatusOrdem.Finalizada)
            classe += 'badge-success';
        else if (status == StatusOrdem.Cancelada)
            classe += 'badge-danger';
        return classe;
    }

    public static getDescricaoStatus(status: string): string {
        return StatusOrdem[status];
    }

    public static getDescricaoUnidadeMedida(tipo: string) {
        return StatusOrdem[tipo];
    }

    public static getDescricaoMotivo(motivo: string): string {
        return OrdemMotivoExpedicaoDetalhe[motivo];
    }
}