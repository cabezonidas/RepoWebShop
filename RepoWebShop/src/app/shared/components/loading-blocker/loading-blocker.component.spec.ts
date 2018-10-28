import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { LoadingBlockerComponent } from './loading-blocker.component';

describe('LoadingBlockerComponent', () => {
  let component: LoadingBlockerComponent;
  let fixture: ComponentFixture<LoadingBlockerComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ LoadingBlockerComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LoadingBlockerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
