import { Component } from '@angular/core';
import { AuthRoutingModule } from "../../auth/auth-routing-module";
import { SearchForm } from './search-form/search-form';

@Component({
  selector: 'app-main-header',
  imports: [AuthRoutingModule, SearchForm],
  templateUrl: './main-header.html',
  styleUrl: './main-header.scss'
})
export class MainHeader {

}
