export class DataHelper {


    public static getDataExtenso(data: Date) : string {
        var dia = data.getUTCDate();
        var diaSemana = data.getDay();
        var mes = data.getMonth();
        var ano = data.getFullYear();
        
        // Resultado
        return  `${DataExtenso.diasSemana[diaSemana]}, ${dia} de ${DataExtenso.meses[mes]} de ${ano}.`;
      }
}


export class DataExtenso {

    public static meses = ['Janeiro', 'Fevereiro', 'Março', 'Abril', 'Maio', 'Junho', 'Julho', 'Agosto', 'Setembro', 'Outubro', 'Novembro', 'Dezembro'];
    public static diasSemana = ['Domingo', 'Segunda-feira', 'Terça-feira', 'Quarta-feira', 'Quinta-feira', 'Sexta-feira', 'Sábado'];
}