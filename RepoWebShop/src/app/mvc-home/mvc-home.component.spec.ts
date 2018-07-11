import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MvcHomeComponent } from './mvc-home.component';

describe('MvcHomeComponent', () => {
  let component: MvcHomeComponent;
  let fixture: ComponentFixture<MvcHomeComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MvcHomeComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MvcHomeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
