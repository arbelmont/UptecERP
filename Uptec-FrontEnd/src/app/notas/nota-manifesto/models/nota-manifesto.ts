
export class NotaManifesto {

    nomeEmitente: string;
    documentoEmitente: string;
    chaveNfe: string;
    valorTotal: number;
    dataEmissao: Date;
    situacao: number;
    manifestacaoDestinatario: number;
    justificativaManifestacao: string;
    nfeCompleta: boolean;
    tipoNfe: number;
    versao: string;
    digestValue: string;
    numeroCartaCorrecao: string;
    cartaCorrecao: string;
    dataCartaCorrecao: string;
    dataCancelamento: string;
    justificativaCancelamento: string;
    dataInclusao: Date;
    dataManifestacao: string;
    notificacao: string;
    id: string;
    validation: string;
    deleted: boolean;
    manifestacaoUsuario: number;
}

export class UsuarioManifesto {
    id: string;
    chaveNfe: string;
    manifestacaoDestinatario: number;
    justificativa: string;
}