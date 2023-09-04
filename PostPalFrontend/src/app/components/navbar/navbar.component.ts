import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { RoutePaths } from '../../app-routing.module';
import { ProfileService } from '../../services/profile.service';
import { UserService } from '../../services/user.service';

@Component({
	selector: 'pal-navbar',
	templateUrl: './navbar.component.html',
	styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent implements OnInit {
	public readonly isLoggedIn$ = this.userService.isLoggedIn$;

	public readonly myProfile$ = this.profileService.currentProfile$;

	constructor(private readonly userService: UserService, private readonly profileService: ProfileService, private readonly router: Router) { }

	ngOnInit(): void {
	}

	public logout(): void {
		this.userService.logout();
		this.router.navigateByUrl(RoutePaths.login);
	}
}
