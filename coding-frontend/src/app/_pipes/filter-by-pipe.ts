import { PipeTransform, Pipe, Injectable } from '@angular/core';

@Pipe({
    name: 'FilterByTypePipe',
    pure: false
})

@Injectable()
export class FilterByTypePipe implements PipeTransform {
    transform(items: any[], field : string, value : string): any[] {
      if (!items) return [];
      if (!value || value.length == 0) return items;
      if (value == 'all') return items
      return items.filter(it =>
      it[field].toLowerCase().indexOf(value.toLowerCase()) !=-1);
    }
}
