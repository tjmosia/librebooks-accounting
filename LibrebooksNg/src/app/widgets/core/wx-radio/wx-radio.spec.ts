import { ComponentFixture, TestBed } from '@angular/core/testing';

import { WxRadio } from './wx-radio';

describe('WxRadio', () => {
  let component: WxRadio;
  let fixture: ComponentFixture<WxRadio>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [WxRadio],
    }).compileComponents();

    fixture = TestBed.createComponent(WxRadio);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
