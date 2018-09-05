import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { LeftnavContainerComponent } from './leftnav-container.component';

describe('LeftnavContainerComponent', () => {
  let component: LeftnavContainerComponent;
  let fixture: ComponentFixture<LeftnavContainerComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ LeftnavContainerComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LeftnavContainerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
