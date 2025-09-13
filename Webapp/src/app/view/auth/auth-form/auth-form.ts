import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';

@Component({
  selector: 'auth-form',
  standalone: false,
  templateUrl: './auth-form.html',
  styleUrl: './auth-form.scss'
})
export class AuthForm implements OnInit {
  protected authForm!: FormGroup;

  public ngOnInit(): void {
    this.authForm = new FormGroup({
      email: new FormControl('', [Validators.required, Validators.email]),
      password: new FormControl('', [Validators.required, Validators.minLength(6)])
    });
  }

  protected onSubmit() {
    if (this.authForm.valid) {
      console.log(this.authForm.value); // envoyer à l’API
    } else {
      console.log("Formulaire invalide");
    }
  }

}
