import { Injectable } from '@angular/core';
import { tap, Observable, map, BehaviorSubject } from 'rxjs';
import { UserAuthRequestDto } from '../models/dtos/user/user-auth-request.dto';
import { UserAuthResponseDto } from '../models/dtos/user/user-auth-response.dto';
import { User } from '../models/interfaces/user';
import { deserializeUser } from '../utils/deserialization.util';
import { deleteToken, getToken, setToken } from '../utils/token.util';
import { ApiService } from './api.service';

@Injectable({
	providedIn: 'root'
})
export class UserService {
	private readonly route = 'users';

	private readonly IsLoggedInSubject = new BehaviorSubject<boolean>(getToken() !== null);
	public readonly isLoggedIn$ = this.IsLoggedInSubject.asObservable();

	constructor(private readonly apiService: ApiService) { }

	public login(dto: UserAuthRequestDto): Observable<UserAuthResponseDto> {
		return this.apiService.post<UserAuthResponseDto>(`${this.route}/login`, dto).pipe(
			tap((response: UserAuthResponseDto) => setToken(response.token)),
			tap(() => this.IsLoggedInSubject.next(true))
		);
	}

	public logout(): void {
		deleteToken();
		this.IsLoggedInSubject.next(false);
	}

	public getAll(): Observable<User[]> {
		return this.apiService.get<User[]>(`${this.route}`).pipe(map(users => users.map(deserializeUser)));
	}
}
