import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './components/login/login.component';

export const routePaths = {
	home: '/',
	login: 'login',
}

const routes: Routes = [
	{ path: routePaths.login, component: LoginComponent },
	{ path: '', redirectTo: routePaths.home, pathMatch: 'full' },
];

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    RouterModule.forRoot(routes)
  ]
})
export class AppRoutingModule { }
