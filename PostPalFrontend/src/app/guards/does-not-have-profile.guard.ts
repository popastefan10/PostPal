import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { map, Observable, of } from 'rxjs';
import { RoutePaths } from '../app-routing.module';
import { ProfileService } from '../services/profile.service';

@Injectable({
	providedIn: 'root'
})
export class DoesNotHaveProfileGuard implements CanActivate {
	constructor(private readonly router: Router, private readonly profileService: ProfileService) { }

	public canActivate(
		route: ActivatedRouteSnapshot,
		state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {

		if (this.profileService.currentProfileExists) {
			return of(this.router.createUrlTree([RoutePaths.myProfile]));
		}
		return this.profileService.hasProfile()
			.pipe(map(exists => exists ? this.router.createUrlTree([RoutePaths.myProfile]) : true));
	}
}
