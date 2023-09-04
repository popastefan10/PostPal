import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { filter, map, of, switchMap } from 'rxjs';
import { PostService } from '../../services/post.service';
import { ProfileService } from '../../services/profile.service';

@Component({
	selector: 'pal-post',
	templateUrl: './post.component.html',
	styleUrls: ['./post.component.scss']
})
export class PostComponent implements OnInit {
	public readonly postId$ = this.route.params.pipe(map(params => params['id'] as string | undefined));

	public readonly post$ = this.postId$.pipe(
		filter(id => id !== undefined),
		switchMap(id => this.postService.getById(id!))
	);

	public readonly profile$ = this.post$.pipe(
		switchMap(post => this.profileService.getByUserId(post.userId))
	);

	constructor(
		private readonly route: ActivatedRoute,
		private readonly postService: PostService,
		private readonly profileService: ProfileService
	) { }

	ngOnInit(): void {
		this.post$.subscribe(console.log);
		this.profile$.subscribe(console.log);
	}
}
