import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DbCleanerComponent } from './db-cleaner.component';

describe('DbCleanerComponent', () => {
  let component: DbCleanerComponent;
  let fixture: ComponentFixture<DbCleanerComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DbCleanerComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DbCleanerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
