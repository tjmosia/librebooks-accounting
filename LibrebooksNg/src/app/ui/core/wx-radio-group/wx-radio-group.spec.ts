import { ComponentFixture, TestBed } from '@angular/core/testing';

import { WxRadioGroup } from './wx-radio-group';

describe('WxRadioGroup', () => {
  let component: WxRadioGroup;
  let fixture: ComponentFixture<WxRadioGroup>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [WxRadioGroup],
    }).compileComponents();

    fixture = TestBed.createComponent(WxRadioGroup);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
