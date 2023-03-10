import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'price'
})
export class PricePipe implements PipeTransform {

  transform(value: string | undefined): string {
    if (!value) {
      return '';
    }

    if (['Free', 'Exchange'].includes(value)) {
      return value;
    }

    const currencyList = [
      { key: 'uah', display: '&#8372;' },
      { key: 'usd', display: '&dollar;' },
      { key: 'eur', display: '&euro;' }
    ];

    let temp = value.split(' ');

    for (let obj of currencyList) {
      if (temp[1] === obj.key) {
        return `${temp[0]} ${obj.display}`
      }
    }
    
    return value;
  }
}