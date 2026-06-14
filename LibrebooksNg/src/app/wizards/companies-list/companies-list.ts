import { Component } from '@angular/core';
import { Title } from '@angular/platform-browser';

@Component({
  selector: 'companies-list',
  imports: [],
  templateUrl: './companies-list.html',
  styleUrl: './companies-list.scss',
})
export class CompaniesList {
  constructor(private title: Title) {
    title.setTitle('Companies');
  }
}
