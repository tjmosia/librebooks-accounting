import { Component, computed, input } from '@angular/core';
import { Intent } from '../contants/intent';

@Component({
  selector: 'wx-form-group',
  imports: [],
  templateUrl: './wx-form-group.html',
  styleUrl: './wx-form-group.scss',
})
export class WxFormGroup {
  label = input<string | undefined>();
  validationMessage = input<string | undefined>();
  validationState = input<Intent | null>();
  helperText = input<string | undefined>();
  required = input<boolean>();
  id = input<string | undefined>();
  htmlFor = input<string | undefined>();
  intent = computed(() => {
    if (this.validationState()) {
      return "wx--" + this.validationState()
    }
    return ""
  })
}
