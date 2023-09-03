import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';
import { CreateProfileComponent } from './components/create-profile/create-profile.component';
import { TestComponent } from './components/test/test.component';
import { HasProfileGuard } from './guards/has-profile.guard';
import { DoesNotHaveProfileGuard } from './guards/does-not-have-profile.guard';
import { ProfileComponent } from './components/profile/profile.component';
import { IsLoggedInGuard } from './guards/is-logged-in.guard';

export const RoutePaths = {
	home: '/',
	login: 'login',
	register: 'register',
	createProfile: 'create-profile',
	myProfile: 'profile/me',
	profile: 'profile',
	test: 'test'
}

const routes: Routes = [
	{ path: RoutePaths.login, component: LoginComponent },
	{ path: RoutePaths.register, component: RegisterComponent },
	{ path: RoutePaths.createProfile, component: CreateProfileComponent, canActivate: [IsLoggedInGuard, DoesNotHaveProfileGuard] },
	{ path: RoutePaths.myProfile, component: ProfileComponent, canActivate: [IsLoggedInGuard, HasProfileGuard] },
	{ path: RoutePaths.profile + '/:id', component: ProfileComponent },
	{ path: RoutePaths.test, component: TestComponent },
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
