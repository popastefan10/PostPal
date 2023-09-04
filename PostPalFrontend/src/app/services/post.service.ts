import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CommentsWithProfilesDto } from '../models/dtos/comment/comments-with-profiles.dto';
import { PostCreateDto } from '../models/dtos/post/post-create.dto';
import { PostUpdateDto } from '../models/dtos/post/post-update.dto';
import { PostsWithProfilesDto } from '../models/dtos/post/posts-with-profiles.dto';
import { Post } from '../models/interfaces/post';
import { UserProfile } from '../models/interfaces/user-profile';
import { ApiService } from './api.service';

@Injectable({
	providedIn: 'root'
})
export class PostService {
	private readonly route = 'posts';

	constructor(private readonly apiService: ApiService) { }

	public create(dto: PostCreateDto): Observable<Post> {
		const formData = new FormData();
		formData.append('description', dto.description);
		dto.images.forEach(image => formData.append('images', image));

		return this.apiService.post<Post>(`${this.route}`, formData);
	}

	public like(id: string): Observable<void> {
		return this.apiService.post<void>(`${this.route}/${id}/like`);
	}

	public removeLike(id: string): Observable<void> {
		return this.apiService.post<void>(`${this.route}/${id}/remove-like`);
	}

	public getAll(): Observable<PostsWithProfilesDto> {
		return this.apiService.get<PostsWithProfilesDto>(`${this.route}`);
	}

	public getById(id: string): Observable<Post> {
		return this.apiService.get<Post>(`${this.route}/${id}`);
	}

	public getLikesProfiles(id: string): Observable<UserProfile[]> {
		return this.apiService.get<UserProfile[]>(`${this.route}/${id}/likes`);
	}

	public getLikesCount(id: string): Observable<number> {
		return this.apiService.get<number>(`${this.route}/${id}/likes/count`);
	};

	public getCommentsWithProfiles(id: string): Observable<CommentsWithProfilesDto> {
		return this.apiService.get<CommentsWithProfilesDto>(`${this.route}/${id}/comments`);
	}

	public getCommentsCount(id: string): Observable<number> {
		return this.apiService.get<number>(`${this.route}/${id}/comments/count`);
	}

	public update(id: string, dto: PostUpdateDto): Observable<Post> {
		return this.apiService.patch<Post, PostUpdateDto>(`${this.route}/${id}`, dto);
	}

	public delete(id: string): Observable<Post> {
		return this.apiService.delete<Post>(`${this.route}/${id}`);
	}
}
