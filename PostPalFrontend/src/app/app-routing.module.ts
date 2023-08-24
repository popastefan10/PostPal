import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';

export const RoutePaths = {
	home: '/',
	login: 'login',
	register: 'register',
}

const routes: Routes = [
	{ path: RoutePaths.login, component: LoginComponent },
	{ path: RoutePaths.register, component: RegisterComponent },
	{ path: '', redirectTo: RoutePaths.home, pathMatch: 'full' },
];

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    RouterModule.forRoot(routes)
  ]
})
export class AppRoutingModule { }
