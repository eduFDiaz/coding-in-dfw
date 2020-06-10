import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, throwError, BehaviorSubject } from 'rxjs';
import { Product } from '../_models/Product';
import { map, catchError } from 'rxjs/operators';
import { UserService } from './user.service';

import { Requirement } from '../_models/Requirement';
import { environment } from 'src/environments/environment'

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  private currentProduct = new BehaviorSubject<Product[]>([])

  updatedProduct = this.currentProduct.asObservable();

  constructor(private http: HttpClient, private user: UserService) { }


  getProducts(userid: string): Observable<Product[]> {
    return this.http.get<Product[]>(environment.apiUrl + '/product/foruser/' + userid).pipe(
      map((result) => {
        return result
      }, catchError(err => {
        return err
      }))
    )
  }

  getProduct(id: string): Observable<Product> {
    return this.http.get<Product>(environment.apiUrl + '/product/' + id)
  }

  deleteProduct(productid: string): Observable<boolean> {
    // tslint:disable-next-line: object-literal-key-quotes
    return this.http.delete<boolean>(environment.apiUrl + '/product/' + productid + '/delete', { headers: { 'authorization': 'Bearer ' + localStorage.getItem('token') } }).pipe(
      map((result: boolean) => {
        // tslint:disable-next-line: no-shadowed-variable
        this.getProducts(this.user.getCurrentUserId()).subscribe((result) => {
          this.currentProduct.next(result)
        })
        return result
      }, catchError(error => {
        return throwError(error)
      }))
    )
  }

  editProduct(productid: string, newdata: any): Observable<Product> {
    return this.http.put<Product>(environment.apiUrl
      + '/product/' + productid + '/update', newdata, {
      headers: {
        'Content-Type': 'application/json',
        'authorization': 'Bearer ' + localStorage.getItem('token')
      }
    })
    // })
    //   .pipe(
    //     map((response: Product) => {
    //       this.getProducts(this.user.getCurrentUserId()).subscribe((result) => {
    //         this.currentProduct.next(result)
    //         console.log(result)
    //       })
    //       return response
    //     }, catchError(err => {
    //       return err
    //     }))
    //   )
  }

  addProduct(data: Product): Observable<Product> {
    // tslint:disable-next-line: object-literal-key-quotes
    return this.http.post<Product>(environment.apiUrl + '/product/create', data).pipe(
      map((result: any) => {
        return result;
      }), catchError(error => {
        return throwError(error);
        // return error

      })
    )
  }

  addRequirement(data: Requirement): Observable<Requirement> {
    return this.http.post<Requirement>(environment.apiUrl + '/product/addRequirement', data).pipe(
      map((result: Requirement) => {
        return result;
      }), catchError(error => {
        return throwError(error);
        // return error
      })
    )
  }

  deleteRequirement(id: string) {
    return this.http.delete(environment.apiUrl + '/Product/requirement/' + id + '/delete')
  }

  getRequirement(id: string) {
    return this.http.get(environment.apiUrl + '/products/requirement' + id)
  }
}

