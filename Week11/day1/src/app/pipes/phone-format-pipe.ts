import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'phoneFormat',
})
export class PhoneFormatPipe implements PipeTransform {
  transform(value: string): string {
    if (!value) return value;

    return value.replace(/(\d{3})(\d{3})(\d{4})/, '$1-$2-$3');
  }
}
