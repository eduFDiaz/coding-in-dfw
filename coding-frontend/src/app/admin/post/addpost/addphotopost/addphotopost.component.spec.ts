import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AddphotopostComponent } from './addphotopost.component';

describe('AddphotopostComponent', () => {
  let component: AddphotopostComponent;
  let fixture: ComponentFixture<AddphotopostComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AddphotopostComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddphotopostComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
