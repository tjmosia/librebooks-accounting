import { ComponentFixture, TestBed } from '@angular/core/testing';

import { WxCheckbox } from './wx-checkbox';

describe('WxCheckbox', () => {
  let component: WxCheckbox;
  let fixture: ComponentFixture<WxCheckbox>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [WxCheckbox],
    }).compileComponents();

    fixture = TestBed.createComponent(WxCheckbox);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
