import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ListService {

  public lastComponent: string;
  public searchText: string = "";
  public currentPage: number = 1;
  public totalItems: number = 0;
  public maxSize: number = 8;
  public itemsPerPage: number = 15;
  public boundaryLinks: boolean = true;

  public searchText2: string = "";
  public currentPage2: number = 1;
  public totalItems2: number = 0;
  public maxSize2: number = 90;
  public itemsPerPage2: number = 100;
  public boundaryLinks2: boolean = true;
  
  constructor() { }
}