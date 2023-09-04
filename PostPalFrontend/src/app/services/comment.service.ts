import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CommentCreateDto } from '../models/dtos/comment/comment-create.dto';
import { CommentWithProfileDto } from '../models/dtos/comment/comment-with-profile.dto';
import { Comment } from '../models/interfaces/comment';
import { ApiService } from './api.service';
import { UserService } from './user.service';

@Injectable({
  providedIn: 'root'
})
export class CommentService {
	private readonly route: string = 'comments';

	constructor(private readonly apiService: ApiService, private readonly userService: UserService) { }

	public create(dto: Omit<CommentCreateDto, 'userId'>): Observable<Comment> {
		const userId = this.userService.currentUser?.id;
		if (!userId) {
			throw new Error("You must be logged in to create a comment");
		}

		return this.apiService.post<Comment, CommentCreateDto>(this.route, { ...dto, userId });
	}

	public delete(id: string): Observable<CommentWithProfileDto> {
		return this.apiService.delete<CommentWithProfileDto>(`${this.route}/${id}`);
	}
}
