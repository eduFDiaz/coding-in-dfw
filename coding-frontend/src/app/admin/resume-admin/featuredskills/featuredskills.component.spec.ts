import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { FeaturedskillsComponent } from './featuredskills.component';

describe('FeaturedskillsComponent', () => {
  let component: FeaturedskillsComponent;
  let fixture: ComponentFixture<FeaturedskillsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FeaturedskillsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FeaturedskillsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
