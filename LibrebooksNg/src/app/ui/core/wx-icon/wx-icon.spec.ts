import { ComponentFixture, TestBed } from '@angular/core/testing';

import { WxIcon } from './wx-icon';

describe('WxIcon', () => {
  let component: WxIcon;
  let fixture: ComponentFixture<WxIcon>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [WxIcon],
    }).compileComponents();

    fixture = TestBed.createComponent(WxIcon);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
