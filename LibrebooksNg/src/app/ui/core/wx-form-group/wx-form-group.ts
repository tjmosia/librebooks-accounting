import { Component, computed, input } from '@angular/core';
import { Intent, intentClasses, intents } from '../contants/intent';

@Component({
  selector: 'wx-form-group',
  imports: [],
  templateUrl: './wx-form-group.html',
  styleUrl: './wx-form-group.scss',
})
export class WxFormGroup {
  rootClass = 'wx-form-group';
  label = input<string | undefined>();
  validationMessage = input<string | undefined>();
  validationState = input<Intent>(intents.none);
  helperText = input<string | undefined>();
  required = input<boolean>();
  id = input<string | undefined>();
  htmlFor = input<string | undefined>();

  computeContainerClassName() {
    let _class = this.rootClass + ' ';
    _class += intentClasses[this.validationState()];
    return _class;
  }
}
