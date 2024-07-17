/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { FeaturedSkillsService } from './featured-skills.service';

describe('Service: FeaturedSkills', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [FeaturedSkillsService]
    });
  });

  it('should ...', inject([FeaturedSkillsService], (service: FeaturedSkillsService) => {
    expect(service).toBeTruthy();
  }));
});
