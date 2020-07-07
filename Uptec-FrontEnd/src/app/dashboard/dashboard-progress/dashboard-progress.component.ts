import { Component, OnInit, Input, OnChanges, SimpleChanges } from '@angular/core';

@Component({
  selector: 'app-dashboard-progress',
  templateUrl: './dashboard-progress.component.html'
})
export class DashboardProgressComponent implements OnInit, OnChanges {
  
  @Input() entrada: number
  @Input() saldo: number;

  percent: number = 0;

  constructor() { }

  ngOnInit() {
    this.percent = (this.saldo / this.entrada) * 100;
  }

  ngOnChanges(changes: SimpleChanges): void {
    //this.calculaLevel();
    //console.log("mudou");
  }

  /* private calculaLevel() {
    this.level = 100 - ((this.quantidadeMinima / this.quantidade) * 100);
    if(this.level < 0)
      this.level = 5;

    if(this.level > 20 && this.level < 50)
      this.levelClass = "progress-bar bg-warning";
    else if(this.level >= 50)
      this.levelClass = "progress-bar bg-success";
    else
      this.levelClass = "progress-bar bg-danger";
  } */
}
