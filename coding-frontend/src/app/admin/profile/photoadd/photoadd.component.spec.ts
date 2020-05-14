import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PhotoaddComponent } from './photoadd.component';

describe('PhotoaddComponent', () => {
  let component: PhotoaddComponent;
  let fixture: ComponentFixture<PhotoaddComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PhotoaddComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PhotoaddComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
