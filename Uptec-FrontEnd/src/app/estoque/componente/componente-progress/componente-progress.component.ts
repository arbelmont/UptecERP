import { Component, OnInit, Input, OnChanges, SimpleChanges } from '@angular/core';

@Component({
  selector: 'app-componente-progress',
  templateUrl: './componente-progress.component.html'
})
export class ComponenteProgressComponent implements OnInit, OnChanges {
  
  @Input() quantidade: number
  @Input() quantidadeMinima: number;

  level = 5;
  levelClass = "progress-bar bg-danger";

  constructor() { }

  ngOnInit() {
    
  }

  ngOnChanges(changes: SimpleChanges): void {
    this.calculaLevel();
    //console.log("mudou");
  }

  private calculaLevel() {
    this.level = 100 - ((this.quantidadeMinima / this.quantidade) * 100);
    if(this.level < 0)
      this.level = 5;

    if(this.level > 20 && this.level < 50)
      this.levelClass = "progress-bar bg-warning";
    else if(this.level >= 50)
      this.levelClass = "progress-bar bg-success";
    else
      this.levelClass = "progress-bar bg-danger";
  }
}
