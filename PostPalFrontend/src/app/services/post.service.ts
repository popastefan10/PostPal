import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Post } from '../models/interfaces/post';
import { ApiService } from './api.service';

@Injectable({
	providedIn: 'root'
})
export class PostService {
	private readonly route = 'posts';

	constructor(private readonly apiService: ApiService) { }

	public getAll(): Observable<Post[]> {
		return this.apiService.get<Post[]>(`${this.route}`);
	}
}
