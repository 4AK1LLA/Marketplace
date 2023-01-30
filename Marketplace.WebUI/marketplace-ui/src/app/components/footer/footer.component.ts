import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-footer',
  templateUrl: './footer.component.html',
  styleUrls: ['./footer.component.scss']
})
export class FooterComponent implements OnInit {

  links:{ name: string, ref: string }[] = [];

  ngOnInit(): void {
    this.links = [
      { name: 'Home', ref: '' }, 
      { name: 'Link', ref: '#' }, 
      { name: 'Link', ref: '#' }
    ]
  }

}