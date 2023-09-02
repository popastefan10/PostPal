import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { catchError, map, Observable, of } from 'rxjs';
import { ProfileService } from '../services/profile.service';

@Injectable({
	providedIn: 'root'
})
export class HasProfileGuard implements CanActivate {
	constructor(private readonly router: Router, private readonly profileService: ProfileService) { }

	public canActivate(
		route: ActivatedRouteSnapshot,
		state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {

		if (this.profileService.currentProfileExists) {
			return of(true);
		}
		return this.profileService.hasProfile()
			.pipe(map(exists => exists ? true : this.router.createUrlTree(['/create-profile'])));
	}
}
