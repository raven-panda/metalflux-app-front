import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { MainHeader } from '../../../shared/components/main-header/main-header';

@Component({
  selector: 'app-public-layout',
  imports: [MainHeader, RouterOutlet],
  templateUrl: './public-layout.html',
  styleUrl: './public-layout.scss'
})
export class PublicLayout {

}
