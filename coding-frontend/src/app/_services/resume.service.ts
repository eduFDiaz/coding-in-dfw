import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';

import { map, catchError } from 'rxjs/operators';
import { UserService } from './user.service';
import { Language } from '../_models/Language';
import { Skill } from '../_models/Skill';



@Injectable({
  providedIn: 'root'
})
export class ResumeService {

  baseUrl = 'http://localhost:5050/api'

  constructor(private http: HttpClient, private user: UserService) { }

  getLanguages(userid: string): Observable<any> {
    return this.http.get<any>(this.baseUrl + '/language/foruser/' + userid).pipe(
      map((languages) => {
        return languages
      }, catchError(err => {
        return err
      }))
    )
  }

  addLanguage(langdata: any): Observable<any> {
    return this.http.post(this.baseUrl + '/language/create', langdata, {
      headers: {
        'Content-Type': 'application/json',
        // tslint:disable-next-line: object-literal-key-quotes
        'authorization': 'Bearer ' + localStorage.getItem('token')
      }
    }).pipe(
      map(
        (result) => {
          return result
        }
      ))
  }

  deleteLanguage(languageid: string) {
    return this.http.delete(this.baseUrl + '/language/' + languageid + '/delete', {
      headers: {
        'Content-Type': 'application/json',
        // tslint:disable-next-line: object-literal-key-quotes
        'authorization': 'Bearer ' + localStorage.getItem('token')
      }
    }).pipe(map(
      (result) => {
        return result
      }, catchError(err => {
        return err
      })
    ))
  }

  updateLanguage(lanid: any, langdata: string) {
    return this.http.put(this.baseUrl + '/language/' + lanid + '/update', langdata, {
      headers: {
        'Content-Type': 'application/json-patch+json',
        // tslint:disable-next-line: object-literal-key-quotes
        'authorization': 'Bearer ' + localStorage.getItem('token')
      }
    }).pipe(
      map(
        (result) => {
          return result
        }, catchError(err => {
          return err
        })
      )
    )
  }

  getEducation(userid: string): Observable<any> {
    return this.http.get<any>(this.baseUrl + '/education/foruser/' + userid).pipe(
      map((education) => {
        return education
      }, catchError(err => {
        return err
      }))
    )
  }

  addEducation(data: any): Observable<any> {
    return this.http.post(this.baseUrl + '/education/create', data, {
      headers: {
        'Content-Type': 'application/json',
        // tslint:disable-next-line: object-literal-key-quotes
        'authorization': 'Bearer ' + localStorage.getItem('token')
      }
    }).pipe(
      map(
        (result) => {
          return result
        }
      ))
  }

  deleteEducation(educationid: string) {
    return this.http.delete(this.baseUrl + '/education/' + educationid + '/delete', {
      headers: {
        'Content-Type': 'application/json',
        // tslint:disable-next-line: object-literal-key-quotes
        'authorization': 'Bearer ' + localStorage.getItem('token')
      }
    }).pipe(map(
      (result) => {
        return result
      }, catchError(err => {
        return err
      })
    ))
  }

  updateEducation(eduid: any, data: string) {
    return this.http.put(this.baseUrl + '/education/' + eduid + '/update', data, {
      headers: {
        'Content-Type': 'application/json-patch+json',
        // tslint:disable-next-line: object-literal-key-quotes
        'authorization': 'Bearer ' + localStorage.getItem('token')
      }
    }).pipe(
      map(
        (result) => {
          return result
        }, catchError(err => {
          return err
        })
      )
    )
  }

  getSkills(userid: string): Observable<Skill[]> {
    return this.http.get<Skill[]>(this.baseUrl + '/skill/foruser/' + userid).pipe(
      map((skills) => {
        return skills
      }, catchError(err => {
        return err
      }))
    )
  }

  addSkill(data: any): Observable<Skill> {
    return this.http.post<Skill>(this.baseUrl + '/skill/create', data, {
      headers: {
        'Content-Type': 'application/json',
        // tslint:disable-next-line: object-literal-key-quotes
        'authorization': 'Bearer ' + localStorage.getItem('token')
      }
    }).pipe(
      map(
        (result) => {
          return result
        }
      ))
  }

  deleteSkill(id: string) {
    return this.http.delete(this.baseUrl + '/skill/' + id + '/delete', {
      headers: {
        'Content-Type': 'application/json',
        // tslint:disable-next-line: object-literal-key-quotes
        'authorization': 'Bearer ' + localStorage.getItem('token')
      }
    }).pipe(map(
      (result) => {
        return result
      }, catchError(err => {
        return err
      })
    ))
  }

  updateSkill(id: any, data: string) {
    return this.http.put(this.baseUrl + '/skill/' + id + '/update', data, {
      headers: {
        'Content-Type': 'application/json-patch+json',
        // tslint:disable-next-line: object-literal-key-quotes
        'authorization': 'Bearer ' + localStorage.getItem('token')
      }
    }).pipe(
      map(
        (result) => {
          return result
        }, catchError(err => {
          return err
        })
      )
    )
  }

  getProjects(userid: string): Observable<any> {
    return this.http.get<any>(this.baseUrl + '/project/foruser/' + userid).pipe(
      map((projects) => {
        return projects
      }, catchError(err => {
        return err
      }))
    )
  }

  addProject(data: any): Observable<any> {
    return this.http.post<any>(this.baseUrl + '/project/create', data, {
      headers: {
        'Content-Type': 'application/json',
        // tslint:disable-next-line: object-literal-key-quotes
        'authorization': 'Bearer ' + localStorage.getItem('token')
      }
    }).pipe(
      map(
        (result) => {
          return result
        }
      ))
  }

  deleteProject(id: string) {
    return this.http.delete(this.baseUrl + '/project/' + id + '/delete', {
      headers: {
        'Content-Type': 'application/json',
        // tslint:disable-next-line: object-literal-key-quotes
        'authorization': 'Bearer ' + localStorage.getItem('token')
      }
    }).pipe(map(
      (result) => {
        return result
      }, catchError(err => {
        return err
      })
    ))
  }

  updateProject(id: any, data: string) {
    return this.http.put(this.baseUrl + '/project/' + id + '/update', data, {
      headers: {
        'Content-Type': 'application/json-patch+json',
        // tslint:disable-next-line: object-literal-key-quotes
        'authorization': 'Bearer ' + localStorage.getItem('token')
      }
    }).pipe(
      map(
        (result) => {
          return result
        }, catchError(err => {
          return err
        })
      )
    )
  }

  getAwards(userid: string): Observable<any> {
    return this.http.get<any>(this.baseUrl + '/award/foruser/' + userid).pipe(
      map((awards) => {
        return awards
      }, catchError(err => {
        return err
      }))
    )
  }

  addAward(data: any): Observable<any> {
    return this.http.post<any>(this.baseUrl + '/award/create', data, {
      headers: {
        'Content-Type': 'application/json',
        // tslint:disable-next-line: object-literal-key-quotes
        'authorization': 'Bearer ' + localStorage.getItem('token')
      }
    }).pipe(
      map(
        (result) => {
          return result
        }
      ))
  }

  deleteAward(id: string) {
    return this.http.delete(this.baseUrl + '/award/' + id + '/delete', {
      headers: {
        'Content-Type': 'application/json',
        // tslint:disable-next-line: object-literal-key-quotes
        'authorization': 'Bearer ' + localStorage.getItem('token')
      }
    }).pipe(map(
      (result) => {
        return result
      }, catchError(err => {
        return err
      })
    ))
  }

  updateAward(id: any, data: string) {
    return this.http.put(this.baseUrl + '/award/' + id + '/update', data, {
      headers: {
        'Content-Type': 'application/json-patch+json',
        // tslint:disable-next-line: object-literal-key-quotes
        'authorization': 'Bearer ' + localStorage.getItem('token')
      }
    }).pipe(
      map(
        (result) => {
          return result
        }, catchError(err => {
          return err
        })
      )
    )
  }

  getWe(userid: string): Observable<any> {
    return this.http.get<any>(this.baseUrl + '/workexperience/foruser/' + userid).pipe(
      map((awards) => {
        return awards
      }, catchError(err => {
        return err
      }))
    )
  }

  addWe(data: any): Observable<any> {
    return this.http.post<any>(this.baseUrl + '/workexperience/create', data, {
      headers: {
        'Content-Type': 'application/json',
        // tslint:disable-next-line: object-literal-key-quotes
        'authorization': 'Bearer ' + localStorage.getItem('token')
      }
    }).pipe(
      map(
        (result) => {
          return result
        }
      ))
  }

  deleteWe(id: string) {
    return this.http.delete(this.baseUrl + '/workexperience/' + id + '/delete', {
      headers: {
        'Content-Type': 'application/json',
        // tslint:disable-next-line: object-literal-key-quotes
        'authorization': 'Bearer ' + localStorage.getItem('token')
      }
    }).pipe(map(
      (result) => {
        return result
      }, catchError(err => {
        return err
      })
    ))
  }

  updateWe(id: any, data: string) {
    return this.http.put(this.baseUrl + '/workexperience/' + id + '/update', data, {
      headers: {
        'Content-Type': 'application/json-patch+json',
        // tslint:disable-next-line: object-literal-key-quotes
        'authorization': 'Bearer ' + localStorage.getItem('token')
      }
    }).pipe(
      map(
        (result) => {
          return result
        }, catchError(err => {
          return err
        })
      )
    )
  }




}
