import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AuthRoutingModule } from './auth-routing-module';
import { AuthForm } from './auth-form/auth-form';
import { ReactiveFormsModule } from '@angular/forms';
import { Auth } from './auth';


@NgModule({
  declarations: [
    AuthForm,
    Auth,
  ],
  imports: [
    CommonModule,
    AuthRoutingModule,
    ReactiveFormsModule,
  ]
})
export class AuthModule { }
