import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SeatView } from './seat-view';

describe('SeatView', () => {
  let component: SeatView;
  let fixture: ComponentFixture<SeatView>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SeatView]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SeatView);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
