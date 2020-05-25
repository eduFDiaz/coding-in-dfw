import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ProductphotoAddComponent } from './productphoto-add.component';

describe('ProductphotoAddComponent', () => {
  let component: ProductphotoAddComponent;
  let fixture: ComponentFixture<ProductphotoAddComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ProductphotoAddComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ProductphotoAddComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
