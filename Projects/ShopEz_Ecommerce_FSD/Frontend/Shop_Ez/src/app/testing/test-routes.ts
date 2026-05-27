import { Component } from '@angular/core';
import { Routes } from '@angular/router';

@Component({
  selector: 'app-test-route-dummy',
  standalone: true,
  template: ''
})
export class TestRouteDummyComponent {}

export const testRoutes: Routes = [
  { path: 'home', component: TestRouteDummyComponent },
  { path: 'cart', component: TestRouteDummyComponent },
  { path: '**', component: TestRouteDummyComponent }
];
