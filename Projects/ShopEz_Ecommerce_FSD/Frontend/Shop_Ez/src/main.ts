import { bootstrapApplication } from '@angular/platform-browser';
import { appConfig } from './app/app.config';
import { AppComponent } from './app/app';

// Single light theme only (legacy dark preference removed)
localStorage.removeItem('shopez-theme');
document.documentElement.removeAttribute('data-theme');

bootstrapApplication(AppComponent, appConfig)
  .catch((err) => console.error(err));

