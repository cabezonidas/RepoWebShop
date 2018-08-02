import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { NewCateringShellComponent } from './new-catering-shell.component';

describe('NewCateringShellComponent', () => {
  let component: NewCateringShellComponent;
  let fixture: ComponentFixture<NewCateringShellComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ NewCateringShellComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(NewCateringShellComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
