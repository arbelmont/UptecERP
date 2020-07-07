import { LoteService } from 'app/estoque/lote/services/lote.service';
import { Component, OnInit } from '@angular/core';
import { DataHelper } from 'app/shared/helpers/data-helper';
import { OrdemService } from 'app/producao/ordem/services/ordem.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {

  dataString: string;
  linhasProducao$ = this.ordemService.getLinhaProducao();
  lotesSaldos$ = this.loteService.getSaldos();

  ngOnInit(): void {
    this.dataString = DataHelper.getDataExtenso(new Date);

  }

  constructor(private ordemService: OrdemService, 
              private loteService: LoteService) { }
}
