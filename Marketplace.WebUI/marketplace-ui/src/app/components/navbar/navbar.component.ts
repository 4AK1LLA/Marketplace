import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent implements OnInit {

  currentLocale: string = 'en-US';
  locales = [
    { code: 'en-US', label: 'English', class: 'fi fi-us' },
    { code: 'uk-UA', label: 'Українська', class: 'fi fi-ua' }
  ];

  constructor() {
  }

  ngOnInit(): void {
    this.currentLocale = !($localize.locale === undefined) ? $localize.locale : this.currentLocale;
  }

}
