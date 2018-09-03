import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TrolleyIconComponent } from './trolley-icon.component';

describe('TrolleyIconComponent', () => {
  let component: TrolleyIconComponent;
  let fixture: ComponentFixture<TrolleyIconComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TrolleyIconComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TrolleyIconComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
