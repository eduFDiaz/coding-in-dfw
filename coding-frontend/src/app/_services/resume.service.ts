import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';

import { map, catchError } from 'rxjs/operators';
import { UserService } from './user.service';
import { Language } from '../_models/Language';
import { Skill } from '../_models/Skill';
import { Interest } from '../_models/Interest';
import { environment } from 'src/environments/environment';



@Injectable({
  providedIn: 'root'
})
export class ResumeService {

  constructor(private http: HttpClient, private user: UserService) { }

  getLanguages(userid: string): Observable<any> {
    return this.http.get<any>(environment.apiUrl + '/language/foruser/' + userid).pipe(
      map((languages) => {
        return languages
      }, catchError(err => {
        return err
      }))
    )
  }

  addLanguage(langdata: any): Observable<any> {
    return this.http.post(environment.apiUrl + '/language/create', langdata).pipe(
      map(
        (result) => {
          return result
        }
      ))
  }

  deleteLanguage(languageid: string) {
    return this.http.delete(environment.apiUrl + '/language/' + languageid + '/delete').pipe(map(
      (result) => {
        return result
      }, catchError(err => {
        return err
      })
    ))
  }

  updateLanguage(lanid: any, langdata: string) {
    return this.http.put(environment.apiUrl + '/language/' + lanid + '/update', langdata, {
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
    return this.http.get<any>(environment.apiUrl + '/education/foruser/' + userid).pipe(
      map((education) => {
        return education
      }, catchError(err => {
        return err
      }))
    )
  }

  addEducation(data: any): Observable<any> {
    return this.http.post(environment.apiUrl + '/education/create', data).pipe(
      map(
        (result) => {
          return result
        }
      ))
  }

  deleteEducation(educationid: string) {
    return this.http.delete(environment.apiUrl + '/education/' + educationid + '/delete').pipe(map(
      (result) => {
        return result
      }, catchError(err => {
        return err
      })
    ))
  }

  updateEducation(eduid: any, data: string) {
    return this.http.put(environment.apiUrl + '/education/' + eduid + '/update', data, {
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
    return this.http.get<Skill[]>(environment.apiUrl + '/skill/foruser/' + userid).pipe(
      map((skills) => {
        return skills
      }, catchError(err => {
        return err
      }))
    )
  }

  addSkill(data: any): Observable<Skill> {
    return this.http.post<Skill>(environment.apiUrl + '/skill/create', data).pipe(
      map(
        (result) => {
          return result
        }
      ))
  }

  deleteSkill(id: string) {
    return this.http.delete(environment.apiUrl + '/skill/' + id + '/delete').pipe(map(
      (result) => {
        return result
      }, catchError(err => {
        return err
      })
    ))
  }

  updateSkill(id: any, data: string) {
    return this.http.put(environment.apiUrl + '/skill/' + id + '/update', data, {
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
    return this.http.get<any>(environment.apiUrl + '/project/foruser/' + userid).pipe(
      map((projects) => {
        return projects
      }, catchError(err => {
        return err
      }))
    )
  }

  addProject(data: any): Observable<any> {
    return this.http.post<any>(environment.apiUrl + '/project/create', data).pipe(
      map(
        (result) => {
          return result
        }
      ))
  }

  deleteProject(id: string) {
    return this.http.delete(environment.apiUrl + '/project/' + id + '/delete').pipe(map(
      (result) => {
        return result
      }, catchError(err => {
        return err
      })
    ))
  }

  updateProject(id: any, data: string) {
    return this.http.put(environment.apiUrl + '/project/' + id + '/update', data, {
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
    return this.http.get<any>(environment.apiUrl + '/award/foruser/' + userid).pipe(
      map((awards) => {
        return awards
      }, catchError(err => {
        return err
      }))
    )
  }

  addAward(data: any): Observable<any> {
    return this.http.post<any>(environment.apiUrl + '/award/create', data).pipe(
      map(
        (result) => {
          return result
        }
      ))
  }

  deleteAward(id: string) {
    return this.http.delete(environment.apiUrl + '/award/' + id + '/delete').pipe(map(
      (result) => {
        return result
      }, catchError(err => {
        return err
      })
    ))
  }

  updateAward(id: any, data: string) {
    return this.http.put(environment.apiUrl + '/award/' + id + '/update', data, {
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
    return this.http.get<any>(environment.apiUrl + '/workexperience/foruser/' + userid).pipe(
      map((awards) => {
        return awards
      }, catchError(err => {
        return err
      }))
    )
  }

  addWe(data: any): Observable<any> {
    return this.http.post<any>(environment.apiUrl + '/workexperience/create', data).pipe(
      map(
        (result) => {
          return result
        }
      ))
  }

  deleteWe(id: string) {
    return this.http.delete(environment.apiUrl + '/workexperience/' + id + '/delete').pipe(map(
      (result) => {
        return result
      }, catchError(err => {
        return err
      })
    ))
  }

  updateWe(id: any, data: string) {
    return this.http.put(environment.apiUrl + '/workexperience/' + id + '/update', data, {
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

  getInterests(userid: string): Observable<Interest[]> {
    return this.http.get<any>(environment.apiUrl + '/interest/foruser/' + userid).pipe(
      map((interest) => {
        return interest
      }, catchError(err => {
        return err
      }))
    )
  }

  addInterest(data: Interest): Observable<Interest> {
    return this.http.post<any>(environment.apiUrl + '/interest/create', data).pipe(
      map(
        (result) => {
          return result
        }
      ))
  }

  deleteInterest(id: string) {
    return this.http.delete(environment.apiUrl + '/interest/' + id + '/delete').pipe(map(
      (result) => {
        return result
      }, catchError(err => {
        return err
      })
    ))
  }

  updateInterest(id: string, data: string) {
    return this.http.put(environment.apiUrl + '/interest/' + id + '/update', data, {
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
