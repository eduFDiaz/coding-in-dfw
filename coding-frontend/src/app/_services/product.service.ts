import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, throwError, BehaviorSubject } from 'rxjs';
import { Product } from '../_models/Product';
import { map, catchError } from 'rxjs/operators';
import { UserService } from './user.service';
import { resolve } from 'url';
import { Requirement } from '../_models/Requirement';
import { environment } from 'src/environments/environment'

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  private currentProduct = new BehaviorSubject<Product[]>([])

  updatedProduct = this.currentProduct.asObservable();

  constructor(private http: HttpClient, private user: UserService) { }

  getProducts(userid: number): Observable<Product[]> {
    return this.http.get<Product[]>(environment.apiUrl + '/product/' + userid).pipe(
      map((result) => {
        return result
      }, catchError(err => {
        return err
      }))
    )
  }

  deleteProduct(productid: number): Observable<boolean> {
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

  editProduct(productid: number, newdata: any): Observable<boolean> {
    // tslint:disable-next-line: object-literal-key-quotes
    return this.http.put<boolean>(environment.apiUrl + '/product/' + productid + '/update', newdata, {
      headers: {
        'Content-Type': 'application/json',
        // tslint:disable-next-line: object-literal-key-quotes
        'authorization': 'Bearer ' + localStorage.getItem('token')
      }
    })
      .pipe(
        map((response: boolean) => {
          this.getProducts(this.user.getCurrentUserId()).subscribe((result) => {
            this.currentProduct.next(result)
          })
          return response
        }, catchError(err => {
          return err
        }))
      )
  }

  addProduct(data: Product): Observable<Product> {
    // tslint:disable-next-line: object-literal-key-quotes
    return this.http.post<Product>(environment.apiUrl + '/product/create', data, { headers: { 'authorization': 'Bearer ' + localStorage.getItem('token') } }).pipe(
      map((result: any) => {
        return result;
      }), catchError(error => {
        return throwError(error);
        // return error

      })
    )
  }

  addRequirement(data: Requirement): Observable<Requirement> {
    return this.http.post<Requirement>(environment.apiUrl + '/product/addRequirement', data, { headers: { 'authorization': 'Bearer ' + localStorage.getItem('token') } }).pipe(
      map((result: Requirement) => {
        return result;
      }), catchError(error => {
        return throwError(error);
        // return error
      })
    )
  }




}
