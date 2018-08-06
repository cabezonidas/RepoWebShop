import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CateringOptionsShellComponent } from './catering-options-shell.component';

describe('CateringOptionsShellComponent', () => {
  let component: CateringOptionsShellComponent;
  let fixture: ComponentFixture<CateringOptionsShellComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CateringOptionsShellComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CateringOptionsShellComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
