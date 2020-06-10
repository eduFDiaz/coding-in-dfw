import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { FeaturedSkill } from '../_models/FeaturedSkill';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class FeaturedSkillsService {

  constructor(private http: HttpClient) { }

  newFeaturedSkill(data: FeaturedSkill) {
    return this.http.post(environment.apiUrl + '/featureskill/create', data)
  }

  deleteFeatureSkill(id: string) {
    return this.http.delete(environment.apiUrl + '/featureskill/' + id + '/delete')
  }

  editFeatureSkill(id: string, data: FeaturedSkill) {
    return this.http.put(environment.apiUrl + '/featureskill/' + id + '/update', data)
  }

  getAllFeaturedSkills() {
    return this.http.get(environment.apiUrl + '/featureskill/all')
  }

}
