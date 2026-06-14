import { Component, input } from '@angular/core';

@Component({
  selector: 'wx-icon',
  imports: [],
  templateUrl: './wx-icon.html',
  styleUrl: './wx-icon.scss',
})
export class WxIcon {
  name = input.required<string>();
  size = input<number>(16);
}
