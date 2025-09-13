import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { Home } from './home/home';
import { PublicLayout } from './public-layout/public-layout';

const routes: Routes = [
  {
    path: "",
    component: PublicLayout,
    children: [
      { path: "", component: Home }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PublicRoutingModule { }
