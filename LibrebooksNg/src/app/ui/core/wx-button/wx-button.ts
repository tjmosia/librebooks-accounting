import { Component, computed, input, output } from '@angular/core';
import { } from "@angular/router";
import { WxSpinner } from "../wx-spinner/wx-spinner";
import { NgClass } from '@angular/common';

@Component({
  selector: 'wx-button',
  imports: [WxSpinner],
  templateUrl: './wx-button.html',
  styleUrl: './wx-button.scss',
})
export class WxButton {
  readonly id = input<string | null>(null);
  readonly className = input<string>('');
  readonly text = input<string>('');
  readonly variant = input<Variants>("primary");
  readonly size = input<Sizes>("medium");
  readonly disabled = input<boolean>(false);
  readonly onClick = output<MouseEvent>();
  readonly ariaLabel = input<string | null>(null);
  readonly type = input<'button' | 'submit' | 'reset'>('submit');
  readonly inline = input<boolean>(false);
  readonly loading = input<boolean>(false);
  readonly icon = input<string | undefined>(undefined);

  computeButtonClassName = computed(() => {
    let _className = this.className();
    _className += this.disabled() ? "wx--disabled" : "";
    _className += this.inline() ? ' wx--inline' : "";
    _className += ` wx--${this.variant()}`;
    _className += ` wx--${this.size()}`;
    _className += ` wx--${this.variant()}`;
    return _className;
  });
}

type Sizes = "tiny" | "small" | "medium" | "large";
type Variants = "minimal" | "outlined" | "primary" | "secondary";
