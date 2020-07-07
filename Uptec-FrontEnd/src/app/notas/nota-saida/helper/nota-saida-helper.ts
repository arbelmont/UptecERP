import { DownloadHelper } from './../../../shared/helpers/download-helper';
import { TipoDestinatario } from './../../../shared/enums/tipoDestinatario';
import { UnidadeMedida } from "app/shared/enums/unidadeMedida";
import { StatusNotaSaida } from 'app/shared/enums/statusNotaSaida';
import { NotaSaidaService } from '../services/nota-saida.service';
import { take } from 'rxjs/operators';

export class NotaSaidaHelper {

    public static getStatusClass(status: number): string {
        let classe = 'badge badge-pill text-white ';
        if (status == StatusNotaSaida.Processando)
            classe += 'badge-info';
        else if (status == StatusNotaSaida.Processada)
            classe += 'badge-success';
        else if (status == StatusNotaSaida.Rejeitada)
            classe += 'badge-warning';
        else if (status == StatusNotaSaida.Cancelada)
            classe += 'badge-danger';
        return classe;
    }

    public static getDescricaoStatus(status: string): string {
        return StatusNotaSaida[status];
    }

    public static getDescricaoTipoDestinatario(tipo: string) {
        return TipoDestinatario[tipo];
    }

    public static getDescricaoUnidadeMedida(tipo: string) {
        return UnidadeMedida[tipo];
    }

    public static downloadPdf(numeroNota: string, service: NotaSaidaService) {
        service.downloadPdf(numeroNota)
            .pipe(take(1))
            .subscribe(res => {
                DownloadHelper.handleFile(res, "application/pdf", `${numeroNota}.pdf`);
            });
    }

    public static downloadXml(numeroNota: string, service: NotaSaidaService) {
        service.downloadXml(numeroNota)
            .pipe(take(1))
            .subscribe(res => {
                DownloadHelper.handleFile(res, "application/xml", `${numeroNota}.xml`);
            });
    }
}