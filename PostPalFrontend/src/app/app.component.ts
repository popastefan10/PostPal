import { Component } from '@angular/core';
import { tap } from 'rxjs';
import { PostService } from './services/post.service';
import { ProfileService } from './services/profile.service';
import { UserService } from './services/user.service';

@Component({
	selector: 'app-root',
	templateUrl: './app.component.html',
	styleUrls: ['./app.component.scss']
})
export class AppComponent {
	constructor(
		private readonly postService: PostService,
		private readonly profileService: ProfileService,
		private readonly userService: UserService,
	) { }

	title = 'PostPalFrontend';

	public getAllPosts(): void {
		this.postService.getAll().pipe(tap(console.log)).subscribe();
	}

	public getAllProfiles(): void {
		this.profileService.getAll().pipe(tap(console.log)).subscribe();
	}

	public getProfilesByIds(): void {
		this.profileService.getByIds(['95c38e5e-f81e-4aa9-a25e-08db9e5348a4']).pipe(tap(console.log)).subscribe();
	}

	public getUsers(): void {
		this.userService.getAll().pipe(tap(console.log)).subscribe();
	}

	public getUserMe(): void {
		this.userService.getMe().pipe(tap(console.log)).subscribe();
	}
}
