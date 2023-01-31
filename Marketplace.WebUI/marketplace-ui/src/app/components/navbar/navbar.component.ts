import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent implements OnInit {

  locales = [
    { code: 'en-US', label: 'English', class: 'fi fi-us me-2' },
    { code: 'uk-UA', label: 'Українська', class: 'fi fi-ua me-2' }
  ];

  constructor() { }

  ngOnInit(): void {
  }

}
