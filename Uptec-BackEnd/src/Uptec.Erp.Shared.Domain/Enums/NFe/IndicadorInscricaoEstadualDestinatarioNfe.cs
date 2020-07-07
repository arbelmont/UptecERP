namespace Uptec.Erp.Shared.Domain.Enums.NFe
{
    public enum IndicadorInscricaoEstadualDestinatarioNfe : byte
    {
        ContribuinteICMS = 1, //(informar a IE do destinatário);
        ContribuinteIsentoDeInscricaoNoCadastroDeContribuintesDoICMS = 2,
        NaoContribuinte = 3 //, que pode ou não possuir Inscrição Estadual no Cadastro de Contribuintes do ICMS.
    }
}