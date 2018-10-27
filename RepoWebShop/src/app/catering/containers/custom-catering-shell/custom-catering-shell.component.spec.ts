import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CustomCateringShellComponent } from './custom-catering-shell.component';

describe('CustomCateringShellComponent', () => {
  let component: CustomCateringShellComponent;
  let fixture: ComponentFixture<CustomCateringShellComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CustomCateringShellComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CustomCateringShellComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
