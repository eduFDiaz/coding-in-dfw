import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { FeaturedSkill } from '../_models/FeaturedSkill';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class FeaturedSkillsService {

  myheader = {
    headers: {
      'authorization': 'Bearer ' + localStorage.getItem('token'),
      'Content-Type': 'application/json'
    }
  }


  constructor(private http: HttpClient) { }

  newFeaturedSkill(data: FeaturedSkill) {
    return this.http.post(environment.apiUrl + '/featureskill/create', data, this.myheader)
  }

  deleteFeatureSkill(id: string) {
    return this.http.delete(environment.apiUrl + '/featureskill/' + id + '/delete', this.myheader)
  }

  editFeatureSkill(id: string, data: FeaturedSkill) {
    return this.http.put(environment.apiUrl + '/featureskill/' + id + '/update', data, this.myheader)
  }

  getAllFeaturedSkills() {
    return this.http.get(environment.apiUrl + '/featureskill/all')
  }

}
