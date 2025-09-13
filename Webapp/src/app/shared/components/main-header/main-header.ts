import { Component } from '@angular/core';
import { SearchForm } from './search-form/search-form';
import { AuthRoutingModule } from '../../../view/auth/auth-routing-module';

@Component({
  selector: 'app-main-header',
  imports: [AuthRoutingModule, SearchForm],
  templateUrl: './main-header.html',
  styleUrl: './main-header.scss'
})
export class MainHeader {

}
