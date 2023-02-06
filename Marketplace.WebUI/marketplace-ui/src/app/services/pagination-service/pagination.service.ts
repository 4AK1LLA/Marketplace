import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class PaginationService {

  constructor() { }

  getPaginationArray(currentPage: number, pagesCount: number): number[] {

    if (currentPage <= 0 || currentPage > pagesCount || pagesCount == 1) {
      return [];
    }
    const paginationLimit: number = 5;
    let paginationArray: number[] = [];

    paginationArray = (pagesCount > 0 && pagesCount <= paginationLimit) 
      ? new Array<number>(pagesCount) 
      : new Array<number>(paginationLimit);

    if (currentPage == 1 || currentPage == 2 || currentPage == 3) {
      for (let i = 0; i < paginationArray.length; i++) {
        paginationArray[i] = i + 1;
      }

      console.log(paginationArray)
      return paginationArray;
    }

    if (currentPage == pagesCount || currentPage == pagesCount - 1 || currentPage == pagesCount - 2) {
      let index2 = 0;
      for (let i = paginationArray.length - 1; i >= 0; i--) {
        paginationArray[i] = pagesCount - index2;
        index2++;
      }

      console.log(paginationArray)
      return paginationArray;
    }

    let margin: number = 2;
    for (let i = 0; i < paginationArray.length; i++) {
      paginationArray[i] = currentPage - margin;
      margin--;
    }

    console.log(paginationArray)
    return paginationArray;
  }
}
