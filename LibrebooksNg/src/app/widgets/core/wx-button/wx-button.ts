import { Component, input, output } from '@angular/core';
import { } from "@angular/router";
import { ButtonSize, buttonSizeClasses, buttonSizes, ButtonVariant, buttonVariantClasses, buttonVariants } from '../contants';
import { WxSpinner } from "../wx-spinner/wx-spinner";

@Component({
  selector: 'wx-button',
  imports: [WxSpinner],
  templateUrl: './wx-button.html',
  styleUrl: './wx-button.scss',
})
export class WxButton {
  readonly id = input<string | null>(null)
  readonly class = input<string>("")
  readonly text = input<string>("")
  readonly variant = input<ButtonVariant>(buttonVariants.primary)
  readonly size = input<ButtonSize>(buttonSizes.medium)
  readonly disabled = input<boolean>(false)
  readonly onClick = output<MouseEvent>()
  readonly ariaLabel = input<string | null>(null)
  readonly type = input<"button" | "submit" | "reset">("submit")
  readonly inline = input<boolean>(false)
  readonly loading = input<boolean>(false)

  getVariantClass() {
    return buttonVariantClasses[this.variant()]
  }

  getSizeClass() {
    return buttonSizeClasses[this.size()]
  }

}
