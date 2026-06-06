import { Component, computed, input, model, output } from '@angular/core';

type InputType = "text" | "password" | "email" | "search" | "tel" | "url"
type InputSize = "small" | "medium" | "large"

@Component({
  selector: 'wx-input-group',
  imports: [],
  templateUrl: './wx-input-group.html',
  styleUrl: './wx-input-group.scss',
})
export class WxInputGroup {
  readonly id = input<string | null>();
  readonly value = input<string>();
  readonly onChange = output<{ data: string, event: Event }>();
  readonly class = input<string>("");
  readonly inputClass = input<string>("");
  readonly placeholder = input<string | null>(null);
  readonly name = input<string | null>();
  readonly type = input<InputType>("text");
  readonly outlined = input<boolean>(false);
  readonly required = input<boolean>(false)
  readonly size = input<InputSize>("medium")
  readonly inputId = input<string | null>(null)

  changeHandler(event: Event) {
    this.onChange.emit({ data: (event.target as HTMLInputElement).value, event })
  }

  getInputSizeClass() {
    return 'wx--' + this.size()
  }
}
