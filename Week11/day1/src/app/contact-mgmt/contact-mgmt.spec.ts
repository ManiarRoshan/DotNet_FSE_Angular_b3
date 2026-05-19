import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ContactMgmt } from './contact-mgmt';

describe('ContactMgmt', () => {
  let component: ContactMgmt;
  let fixture: ComponentFixture<ContactMgmt>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ContactMgmt],
    }).compileComponents();

    fixture = TestBed.createComponent(ContactMgmt);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
