import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable, of, tap } from 'rxjs';
import { RoutePaths } from '../app-routing.module';
import { UserService } from '../services/user.service';

@Injectable({
	providedIn: 'root'
})
export class IsLoggedInGuard implements CanActivate {
	constructor(private readonly router: Router, private readonly userService: UserService) { }

	canActivate(
		route: ActivatedRouteSnapshot,
		state: RouterStateSnapshot):
		Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
		return this.userService
			.isMyTokenValid()
			.pipe(tap(isValid => isValid ? of(true) : this.router.createUrlTree([RoutePaths.login])));
	}
}
