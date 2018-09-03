import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TrolleyIconShellComponent } from './trolley-icon-shell.component';

describe('TrolleyIconShellComponent', () => {
  let component: TrolleyIconShellComponent;
  let fixture: ComponentFixture<TrolleyIconShellComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TrolleyIconShellComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TrolleyIconShellComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
