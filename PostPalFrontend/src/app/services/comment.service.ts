import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CommentCreateDto } from '../models/dtos/comment/comment-create.dto';
import { CommentWithProfileDto } from '../models/dtos/comment/comment-with-profile.dto';
import { ApiService } from './api.service';

@Injectable({
  providedIn: 'root'
})
export class CommentService {
	private readonly route: string = 'comments';

	constructor(private readonly apiService: ApiService) { }

	public create(dto: CommentCreateDto): Observable<Comment> {
		return this.apiService.post<Comment, CommentCreateDto>(this.route, dto);
	}

	public delete(id: string): Observable<CommentWithProfileDto> {
		return this.apiService.delete<CommentWithProfileDto>(`${this.route}/${id}`);
	}
}
