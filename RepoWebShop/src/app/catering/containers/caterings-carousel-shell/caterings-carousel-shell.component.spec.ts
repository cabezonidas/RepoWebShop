import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CateringsCarouselShellComponent } from './caterings-carousel-shell.component';

describe('CateringsCarouselShellComponent', () => {
  let component: CateringsCarouselShellComponent;
  let fixture: ComponentFixture<CateringsCarouselShellComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CateringsCarouselShellComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CateringsCarouselShellComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
