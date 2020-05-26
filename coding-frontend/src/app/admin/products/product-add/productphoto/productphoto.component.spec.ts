import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ProductphotoComponent } from './productphoto.component';

describe('ProductphotoComponent', () => {
  let component: ProductphotoComponent;
  let fixture: ComponentFixture<ProductphotoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ProductphotoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ProductphotoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
