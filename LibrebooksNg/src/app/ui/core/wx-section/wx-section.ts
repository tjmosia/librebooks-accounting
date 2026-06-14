import { Component, computed, input } from '@angular/core';

@Component({
  selector: 'wx-section',
  imports: [],
  templateUrl: './wx-section.html',
  styleUrl: './wx-section.scss',
})
export class WxSection {
  title = input<string | undefined>();
  className = input<string>('');
  elevated = input<boolean>(false);
  compact = input<boolean>(false);
  subTitle = input<string | undefined>();
  alignText = input<AlignText>('left');
  appearance = input<SectionAppearance>('outlined');

  computeContainerClassName = computed(() => {
    let _className = this.className();
    _className += `wx--${this.elevated() ? 'elevated' : 'flat'}`;
    _className += this.compact() ? 'wx--compact' : '';
    _className += ` wx--${this.appearance()} `;

    return _className;
  });

  computeHeaderClassName = computed(() => {
    let _className = ` wx--align-${this.alignText()}`;

    return _className;
  });
}

export type SectionAppearance = "outlined" | "minimal";
export type AlignText = "left" | "center" | "right";
