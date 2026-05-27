import { TestBed } from '@angular/core/testing';
import { DomSanitizer } from '@angular/platform-browser';
import { ObjectUrlPipe } from './object-url-pipe';

describe('ObjectUrlPipe', () => {
  it('create an instance', () => {
    TestBed.configureTestingModule({});
    const sanitizer = TestBed.inject(DomSanitizer);
    const pipe = new ObjectUrlPipe(sanitizer);
    expect(pipe).toBeTruthy();
  });
});
