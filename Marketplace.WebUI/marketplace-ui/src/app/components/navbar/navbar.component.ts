import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Router } from '@angular/router';
import { UserContext } from 'src/app/contexts/user.context';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent implements OnInit {

  @Input() userContext!: UserContext;
  @Output() loginEvent = new EventEmitter;
  @Output() logoutEvent = new EventEmitter;
  currentLocale: string = 'en-US';
  locales = [
    { code: 'en-US', label: 'English', class: 'fi fi-us', href: '/en-US/' },
    { code: 'uk-UA', label: 'Українська', class: 'fi fi-ua', href: '/uk-UA/' }
  ];

  constructor(private router: Router) { }

  ngOnInit(): void {
    this.currentLocale = !($localize.locale === undefined) ? $localize.locale : this.currentLocale;
  }

  onProfileClick = () => this.loginEvent.emit();
  onLogoutClick = () => this.logoutEvent.emit();

  hrefChanges() {
    let url = this.router.url;
    this.locales.forEach(l => url.replace('/' + l.code, ''));
    this.locales.forEach(l => l.href = '/' + l.code + url);
  }

}
