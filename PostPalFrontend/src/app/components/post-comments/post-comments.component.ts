import { Component, Input, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { BehaviorSubject, filter, map, Observable, switchMap, tap } from 'rxjs';
import { ProfilesDictionary } from '../../models/dtos/comment/comments-with-profiles.dto';
import { Comment } from '../../models/interfaces/comment';
import { Post } from '../../models/interfaces/post';
import { UserProfile } from '../../models/interfaces/user-profile';
import { CommentService } from '../../services/comment.service';
import { PostService } from '../../services/post.service';
import { UserService } from '../../services/user.service';

@Component({
	selector: 'pal-post-comments',
	templateUrl: './post-comments.component.html',
	styleUrls: ['./post-comments.component.scss']
})
export class PostCommentsComponent implements OnInit, OnChanges {
	@Input() post?: Post;
	@Input() profile?: UserProfile;

	private readonly postSubject = new BehaviorSubject<Post | undefined>(this.post);
	public readonly post$ = this.postSubject.asObservable();

	public readonly commentsWithProfiles$ = this.post$.pipe(
		filter((post) => !!post),
		switchMap((post) => this.postService.getCommentsWithProfiles(post!.id))
	);
	public readonly comments$: Observable<Comment[]> = this.commentsWithProfiles$.pipe(
		map((commentsWithProfiles) => commentsWithProfiles.comments)
	);
	public readonly profiles$: Observable<ProfilesDictionary> = this.commentsWithProfiles$.pipe(
		map((commentsWithProfiles) => commentsWithProfiles.profiles)
	);

	public readonly newCommentsSubject = new BehaviorSubject<Comment[]>([]);
	public readonly newComments$ = this.newCommentsSubject.asObservable();
	public readonly allComments$ = this.comments$.pipe(
		switchMap((comments) => this.newComments$.pipe(
			map((newComments) => [...comments, ...newComments])
		))
	);

	public readonly isLoggedIn$ = this.userService.isLoggedIn$;

	public newComment: string = '';

	constructor(
		private readonly postService: PostService,
		private readonly commentService: CommentService,
		private readonly userService: UserService
	) { }

	public ngOnInit(): void { }

	public ngOnChanges(changes: SimpleChanges): void {
		if (changes['post']) {
			this.postSubject.next(changes['post'].currentValue);
		}
	}

	public addComment(): void {
		if (this.newComment.trim().length > 0) {
			const postId = this.post?.id;
			if (!postId) {
				throw new Error("You must be viewing a post to comment on it!");
			}

			this.commentService.create({ postId, content: this.newComment }).pipe(tap((comment) => {
				const newComments = this.newCommentsSubject.getValue();
				newComments.push(comment);
				this.newCommentsSubject.next(newComments);
				this.newComment = '';
			})).subscribe();
		}
	}
}
