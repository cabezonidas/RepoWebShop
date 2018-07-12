import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ChooseItemComponent } from './choose-item.component';

describe('ChooseItemComponent', () => {
  let component: ChooseItemComponent;
  let fixture: ComponentFixture<ChooseItemComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ChooseItemComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ChooseItemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
