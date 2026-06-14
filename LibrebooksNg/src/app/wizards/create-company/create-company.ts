import { Component } from '@angular/core';
import { Title } from '@angular/platform-browser';

@Component({
  selector: 'create-company',
  imports: [],
  templateUrl: './create-company.html',
  styleUrl: './create-company.scss',
})
export class CreateCompany {
  constructor(private title: Title) {
    title.setTitle('Create your Company');
  }
}
