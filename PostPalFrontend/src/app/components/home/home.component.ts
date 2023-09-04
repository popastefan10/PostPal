import { Component, OnInit } from '@angular/core';
import { map, switchMap } from 'rxjs';
import { RoutePaths } from '../../app-routing.module';
import { PostService } from '../../services/post.service';
import { ProfileService } from '../../services/profile.service';

@Component({
	selector: 'pal-home',
	templateUrl: './home.component.html',
	styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
	public readonly postsWithProfiles$ = this.postService.getAll();
	public readonly posts$ = this.postsWithProfiles$.pipe(map(postsWithProfiles => postsWithProfiles.posts));
	public readonly profiles$ = this.postsWithProfiles$.pipe(map(postsWithProfiles => postsWithProfiles.profiles));

	public readonly postPath = RoutePaths.post;

	constructor(private readonly postService: PostService, private readonly profileService: ProfileService) { }

	ngOnInit(): void {
	}
}
