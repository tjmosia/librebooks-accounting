import { Component, input } from '@angular/core';

type SpinnerSize = "tiny" | "small" | "medium" | "large"

@Component({
  selector: 'wx-spinner',
  imports: [],
  templateUrl: './wx-spinner.html',
  styleUrl: './wx-spinner.scss',
})
export class WxSpinner {
  readonly size = input<SpinnerSize>("medium")

  getSize() {
    if (this.size())
      return 'wx--' + this.size();
    return "";
  }
}
