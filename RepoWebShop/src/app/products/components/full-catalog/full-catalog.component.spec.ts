import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { FullCatalogComponent } from './full-catalog.component';

describe('FullCatalogComponent', () => {
  let component: FullCatalogComponent;
  let fixture: ComponentFixture<FullCatalogComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FullCatalogComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FullCatalogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
