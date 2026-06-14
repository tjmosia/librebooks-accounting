import { Component, input } from '@angular/core';

@Component({
  selector: 'wx-button-group',
  imports: [],
  templateUrl: './wx-button-group.html',
  styleUrl: './wx-button-group.scss',
})
export class WxButtonGroup {
  className = input('');
  direction = input<"horizontal" | "vertical">('horizontal');
  compact = input(false)
  rootClass = "wx-button-group"

}
