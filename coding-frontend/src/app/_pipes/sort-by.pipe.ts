import { Pipe, PipeTransform, Injectable } from '@angular/core';
@Pipe({
  name: 'sortby'
})

@Injectable()
export class SortByPipe implements PipeTransform {

  transform(array: Array<any>, filterField: string, filterValue: any): Array<any> {
    if (!array) return []
    if (filterValue == 'all') {
      return array
    } else
      return array.filter(
        item => item[filterField] == filterValue);
  }
}
