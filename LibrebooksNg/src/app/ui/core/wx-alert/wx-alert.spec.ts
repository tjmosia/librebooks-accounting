import { ComponentFixture, TestBed } from '@angular/core/testing';

import { WxAlert } from './wx-alert';

describe('WxAlert', () => {
  let component: WxAlert;
  let fixture: ComponentFixture<WxAlert>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [WxAlert],
    }).compileComponents();

    fixture = TestBed.createComponent(WxAlert);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
