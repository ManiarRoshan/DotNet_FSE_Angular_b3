import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { Demo } from './demo/demo';
import { ContactMgmt } from './contact-mgmt/contact-mgmt';

@Component({
  selector: 'app-root',
  imports: [Demo,ContactMgmt,],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected readonly title = signal('login');
}
