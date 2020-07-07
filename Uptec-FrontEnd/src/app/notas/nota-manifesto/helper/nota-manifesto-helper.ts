import { StatusNotaEntrada } from "app/shared/enums/statusNotaEntrada";
import { ManifestacaoStatus } from "app/shared/enums/manifestacaoStatus";

export class NotaManifestoHelper {

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
        return ManifestacaoStatus[status];
    }
}