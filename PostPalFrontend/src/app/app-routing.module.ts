import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';
import { CreateProfileComponent } from './components/complete-profile/create-profile.component';
import { TestComponent } from './components/test/test.component';
import { HasProfileGuard } from './guards/has-profile.guard';
import { DoesNotHaveProfileGuard } from './guards/does-not-have-profile.guard';

export const RoutePaths = {
	home: '/',
	login: 'login',
	register: 'register',
	completeProfile: 'create-profile',
}

const routes: Routes = [
	{ path: RoutePaths.login, component: LoginComponent },
	{ path: RoutePaths.register, component: RegisterComponent },
	{ path: RoutePaths.completeProfile, component: CreateProfileComponent, canActivate: [DoesNotHaveProfileGuard] },
	{ path: 'test', component: TestComponent, canActivate: [HasProfileGuard] },
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
