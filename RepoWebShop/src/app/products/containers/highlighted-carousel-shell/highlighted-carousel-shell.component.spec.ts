import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { HighlightedCarouselShellComponent } from './highlighted-carousel-shell.component';

describe('HighlightedCarouselShellComponent', () => {
  let component: HighlightedCarouselShellComponent;
  let fixture: ComponentFixture<HighlightedCarouselShellComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ HighlightedCarouselShellComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(HighlightedCarouselShellComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
