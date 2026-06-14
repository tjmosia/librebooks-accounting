import { Component, computed, input, model, output } from '@angular/core';
import { sizeClasses } from '../contants';

type InputType = "text" | "password" | "email" | "search" | "tel" | "url"
type InputSize = "small" | "medium" | "large"

@Component({
  selector: 'wx-input-group',
  imports: [],
  templateUrl: './wx-input-group.html',
  styleUrl: './wx-input-group.scss',
})
export class WxInputGroup {
  readonly id = input<string | null>()
  readonly value = model<string>("")
  readonly onChange = output<{ value: string, event?: Event }>()
  readonly className = input<string>("")
  readonly inputClass = input<string>("")
  readonly placeholder = input<string | null>(null)
  readonly name = input<string | null>()
  readonly type = input<InputType>("text")
  readonly required = input<boolean>(false)
  readonly size = input<InputSize>("medium")
  readonly inputId = input<string | null>(null)
  readonly disabled = input<boolean>(false)

  computeClassName = computed(() =>{
    let _class = 'wx-input-group ' + this.className()
    _class += sizeClasses[this.size()] ?? ""
    return _class
  })

  handleInput(event: Event) {
    const {value} = event.target as HTMLInputElement
    this.value.set(value);
    this.onChange.emit({ value: value, event });
  }

  getInputSizeClass() {
    return 'wx--' + this.size()
  }
}
