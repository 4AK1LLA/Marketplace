import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class PaginationService {

  getPaginationArray(currentPage: number, pagesCount: number): PaginationItem[] {

    if (!currentPage || !pagesCount || currentPage <= 0 || currentPage > pagesCount || pagesCount == 1)
      return [];

    const paginationLimit: number = 5;
    let paginationArray: PaginationItem[] = [];

    paginationArray = (pagesCount > 0 && pagesCount <= paginationLimit)
      ? new Array<PaginationItem>(pagesCount)
      : new Array<PaginationItem>(paginationLimit);
    for (let i = 0; i < paginationArray.length; i++)
      paginationArray[i] = new PaginationItem;

    if (currentPage == 1 || currentPage == 2 || currentPage == 3) {
      for (let i = 0; i < paginationArray.length; i++) {
        paginationArray[i].value = i + 1;
        if (paginationArray[i].value == currentPage)
          paginationArray[i].isActive = !paginationArray[i].isActive;
      }

      console.log(paginationArray)
      return paginationArray;
    }

    if (currentPage == pagesCount || currentPage == pagesCount - 1 || currentPage == pagesCount - 2) {
      let index2 = 0;
      for (let i = paginationArray.length - 1; i >= 0; i--) {
        paginationArray[i].value = pagesCount - index2;
        index2++;
        if (paginationArray[i].value == currentPage)
          paginationArray[i].isActive = !paginationArray[i].isActive;
      }

      console.log(paginationArray)
      return paginationArray;
    }

    let margin: number = 2;
    for (let i = 0; i < paginationArray.length; i++) {
      paginationArray[i].value = currentPage - margin;
      margin--;
      if (paginationArray[i].value == currentPage)
        paginationArray[i].isActive = !paginationArray[i].isActive;
    }

    console.log(paginationArray)
    return paginationArray;
  }

  calculatePagesCount(itemsCount: number): number {
    const maxItemsPerPage: number = 16;

    return (itemsCount % maxItemsPerPage == 0)
      ? itemsCount / maxItemsPerPage
      : Math.floor(itemsCount / maxItemsPerPage) + 1;
  }
}

class PaginationItem {
  value!: number;
  isActive: boolean = false;
}