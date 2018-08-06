import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CateringOptionComponent } from './catering-option.component';

describe('CateringOptionComponent', () => {
  let component: CateringOptionComponent;
  let fixture: ComponentFixture<CateringOptionComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CateringOptionComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CateringOptionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
