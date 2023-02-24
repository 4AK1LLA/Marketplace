import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent implements OnInit {

  @Input() isAuthenticated: boolean = false;
  @Output() loginEvent = new EventEmitter;
  @Output() logoutEvent = new EventEmitter;
  currentLocale: string = 'en-US';
  locales = [
    { code: 'en-US', label: 'English', class: 'fi fi-us', href: '/en-US/' },
    { code: 'uk-UA', label: 'Українська', class: 'fi fi-ua', href: '/uk-UA/' }
  ];

  constructor(private router: Router) { }

  public ngOnInit(): void {
    this.currentLocale = !($localize.locale === undefined) ? $localize.locale : this.currentLocale;
  }

  public onProfileClick = () => this.loginEvent.emit();
  public onLogoutClick = () => this.logoutEvent.emit();

  public hrefChanges() {
    let url = this.router.url;
    this.locales.forEach(l => url.replace('/' + l.code, ''));
    this.locales.forEach(l => l.href = '/' + l.code + url);
  }

}
