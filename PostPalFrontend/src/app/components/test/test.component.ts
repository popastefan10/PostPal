import { Component } from '@angular/core';
import { tap } from 'rxjs';
import { PostService } from '../../services/post.service';
import { ProfileService } from '../../services/profile.service';
import { UserService } from '../../services/user.service';

@Component({
	selector: 'pal-test',
	templateUrl: './test.component.html',
	styleUrls: ['./test.component.scss']
})
export class TestComponent {
	constructor(
		private readonly postService: PostService,
		private readonly profileService: ProfileService,
		private readonly userService: UserService,
	) { }

	public getAllPosts(): void {
		this.postService.getAll().pipe(tap(console.log)).subscribe();
	}

	// USER TESTS
	public banUserId: string = '';
	public banUser(): void {
		this.userService.ban(this.banUserId).pipe(tap(console.log)).subscribe();
	}

	public removeBanUserId: string = '';
	public removeBanUser(): void {
		this.userService.removeBan(this.removeBanUserId).pipe(tap(console.log)).subscribe();
	}

	public getUsers(): void {
		this.userService.getAll().pipe(tap(console.log)).subscribe();
	}

	public getUserMe(): void {
		this.userService.getMe().pipe(tap(console.log)).subscribe();
	}

	public getPostsUserId: string = '';
	public getPostsByUserId(): void {
		this.userService.getPosts(this.getPostsUserId).pipe(tap(console.log)).subscribe();
	}

	// PROFILES TESTS
	public hasProfile(): void {
		this.profileService.hasProfile().pipe(tap(console.log)).subscribe();
	}

	public getAllProfiles(): void {
		this.profileService.getAll().pipe(tap(console.log)).subscribe();
	}

	public getMeProfile(): void {
		this.profileService.getMe().pipe(tap(console.log)).subscribe();
	}

	public profilesIds: string = '';
	public getProfilesByIds(): void {
		this.profileService.getByIds(this.profilesIds.split(',')).pipe(tap(console.log)).subscribe();
	}
}
