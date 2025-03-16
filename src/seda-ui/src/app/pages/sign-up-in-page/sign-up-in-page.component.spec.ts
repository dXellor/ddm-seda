import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SignUpInPageComponent } from './sign-up-in-page.component';

describe('SignUpInPageComponent', () => {
  let component: SignUpInPageComponent;
  let fixture: ComponentFixture<SignUpInPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SignUpInPageComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SignUpInPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
