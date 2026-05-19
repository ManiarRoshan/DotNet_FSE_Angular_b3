import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Contact } from '../../Models/Contact';
import { CommonModule } from '@angular/common';
import { PhoneFormatPipe } from '../pipes/phone-format-pipe';
import { StatusPipe } from '../pipes/status-pipe';
import { ContactFilterPipe } from '../pipes/contact-filter-pipe';

@Component({
  selector: 'app-contact-mgmt',
  imports: [FormsModule,CommonModule,PhoneFormatPipe,StatusPipe,ContactFilterPipe],
  templateUrl: './contact-mgmt.html',
  styleUrl: './contact-mgmt.css',
})
export class ContactMgmt {

  search: string = '';
  limit: number = 5;

  contacts: Contact[] = [
    { name: 'tony stark', email: 'TONY@STARK.COM', phone: '1234567890', isActive: true },
    { name: 'steve rogers', email: 'CAP@AVENGERS.ORG', phone: '9876543210', isActive: false },
    { name: 'bruce banner', email: 'hulk@smash.com', phone: '5556667777', isActive: true },
    { name: 'natasha romanoff', email: 'nat@spy.com', phone: '4443332222', isActive: true },
    { name: 'thor odinson', email: 'thor@asgard.gov', phone: '8880001111', isActive: false },
    { name: 'wanda maximoff', email: 'wanda@magic.com', phone: '2228884444', isActive: true },
    { name: 'clint barton', email: 'clint@archery.com', phone: '1112223333', isActive: false },
    { name: 'peter parker', email: 'spidey@nyc.com', phone: '9998887777', isActive: true },
    { name: 'stephen strange', email: 'doctor@sanctum.com', phone: '0001112222', isActive: true },
    { name: 'sam wilson', email: 'falcon@avengers.org', phone: '3334445555', isActive: false }
  ];

    toggleStatus(contact: Contact) {
    contact.isActive = !contact.isActive;
  }
}
