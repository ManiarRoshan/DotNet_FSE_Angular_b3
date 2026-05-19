import { Pipe, PipeTransform } from '@angular/core';
import { Contact } from '../../Models/Contact';

@Pipe({
  name: 'contactFilter',
})
export class ContactFilterPipe implements PipeTransform {
  transform(contacts: Contact[],search: string): Contact[] {
    if (!contacts || !search) return contacts;
    return contacts.filter(c => 
      c.name.toLowerCase().includes(search.toLowerCase()) || 
      c.email.toLowerCase().includes(search.toLowerCase())
    );
  }
}
