import { ComponentFixture, TestBed } from '@angular/core/testing';

import { WxSpinner } from './wx-spinner';

describe('WxSpinner', () => {
  let component: WxSpinner;
  let fixture: ComponentFixture<WxSpinner>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [WxSpinner],
    }).compileComponents();

    fixture = TestBed.createComponent(WxSpinner);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
