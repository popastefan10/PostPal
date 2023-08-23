import { Injectable } from '@angular/core';
import { tap, Observable } from 'rxjs';
import { UserAuthRequestDto } from '../models/dtos/user/user-auth-request.dto';
import { UserAuthResponseDto } from '../models/dtos/user/user-auth-response.dto';
import { User } from '../models/interfaces/user';
import { setToken } from '../shared/utils';
import { ApiService } from './api.service';

@Injectable({
	providedIn: 'root'
})
export class UserService {
	private readonly route = 'users';

	constructor(private readonly apiService: ApiService) { }

	public login(dto: UserAuthRequestDto): Observable<UserAuthResponseDto> {
		return this.apiService.post<UserAuthResponseDto>(`${this.route}/login`, dto).pipe(
			tap((response: UserAuthResponseDto) => setToken(response.token))
		);
	}

	public getAll(): Observable<User[]> {
		return this.apiService.get<User[]>(`${this.route}`);
	}
}
