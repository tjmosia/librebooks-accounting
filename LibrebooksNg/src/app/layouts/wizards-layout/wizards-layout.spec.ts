import { ComponentFixture, TestBed } from '@angular/core/testing';

import { WizardsLayout } from './wizards-layout';

describe('WizardsLayout', () => {
  let component: WizardsLayout;
  let fixture: ComponentFixture<WizardsLayout>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [WizardsLayout],
    }).compileComponents();

    fixture = TestBed.createComponent(WizardsLayout);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
