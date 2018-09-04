import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { FullCatalogShellComponent } from './full-catalog-shell.component';

describe('FullCatalogShellComponent', () => {
  let component: FullCatalogShellComponent;
  let fixture: ComponentFixture<FullCatalogShellComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FullCatalogShellComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FullCatalogShellComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
