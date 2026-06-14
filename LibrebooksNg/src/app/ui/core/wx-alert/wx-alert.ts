import {
  Component,
  computed,
  contentChild,
  ContentChildren,
  ElementRef,
  input,
  OnInit,
  output,
  signal,
} from '@angular/core';
import { intentClasses, intents } from '../contants';

@Component({
  selector: 'wx-alert',
  imports: [],
  templateUrl: './wx-alert.html',
  styleUrl: './wx-alert.scss',
})
export class WxAlert {
  title = input<string | null>(null);
  intent = input<Intent>('none');
  onClose = output<void>();
  open = input<boolean | null>();
  active = signal(this.open() ?? true);
  className = input<string>('');
  compact = input(false);
  togglable = input(false);
  rootClassName = 'wx-alert';
  @ContentChildren('alertMessage') alertMessage!: ElementRef;

  computeContainerClassName = computed(() => {
    let _class = this.compact() ? 'wx--compact ' : ' ';
    _class += intentClasses[this.intent()];
    return _class;
  });

  handleClose() {
    this.active.set(false);
    this.onClose?.emit();
  }
}


type Intent = "error" | "success" | "warning" | "info" | "none";
