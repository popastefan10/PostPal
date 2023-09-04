import { Injectable } from '@angular/core';
import { tap, Observable, map, BehaviorSubject, takeUntil, of } from 'rxjs';
import { UserAuthRequestDto } from '../models/dtos/user/user-auth-request.dto';
import { UserAuthResponseDto } from '../models/dtos/user/user-auth-response.dto';
import { UserIsTokenValidDto } from '../models/dtos/user/user-is-token-valid.dto';
import { UserRegisterRequestDto } from '../models/dtos/user/user-register-request.dto';
import { UserRegisterResponseDto } from '../models/dtos/user/user-register-response.dto';
import { UserUpdateDto } from '../models/dtos/user/user-update.dto';
import { Post } from '../models/interfaces/post';
import { User } from '../models/interfaces/user';
import { deserializeUser } from '../utils/deserialization.util';
import { SubscriptionCleanup } from '../utils/subscription-cleanup';
import { deleteToken, getToken, setToken } from '../utils/token.util';
import { ApiService } from './api.service';

@Injectable({
	providedIn: 'root'
})
export class UserService extends SubscriptionCleanup {
	private readonly route = 'users';

	private readonly isLoggedInSubject = new BehaviorSubject<boolean>(false);
	public readonly isLoggedIn$ = this.isLoggedInSubject.asObservable();

	private readonly currentUserSubject = new BehaviorSubject<User | null>(null);
	public readonly currentUser$ = this.currentUserSubject.asObservable();

	constructor(private readonly apiService: ApiService) {
		super();

		this.isMyTokenValid().pipe(tap(isValid => this.isLoggedInSubject.next(isValid))).subscribe();

		this.isLoggedIn$.pipe(takeUntil(this.destroyed$)).subscribe(isLoggedIn => {
			console.log(`User is logged in: ${isLoggedIn}`);
			if (isLoggedIn) {
				this.getMe().subscribe();
			} else {
				this.currentUserSubject.next(null);
			}
		});
	}

	public get currentUser(): User | null {
		return this.currentUserSubject.value;
	}

	public register(dto: UserRegisterRequestDto): Observable<UserRegisterResponseDto> {
		return this.apiService
			.post<UserRegisterResponseDto, UserRegisterRequestDto>(`${this.route}/register`, dto)
			.pipe(
				tap((response: UserRegisterResponseDto) => setToken(response.token)),
				tap(() => this.isLoggedInSubject.next(true))
			);
	}

	public login(dto: UserAuthRequestDto): Observable<UserAuthResponseDto> {
		return this.apiService.post<UserAuthResponseDto>(`${this.route}/login`, dto).pipe(
			tap((response: UserAuthResponseDto) => setToken(response.token)),
			tap(() => this.isLoggedInSubject.next(true))
		);
	}

	public logout(): void {
		deleteToken();
		this.isLoggedInSubject.next(false);
	}

	public isMyTokenValid(): Observable<boolean> {
		const myToken = getToken();
		if (myToken === null) {
			return of(false);
		}

		return this.isTokenValid(myToken)
			.pipe(tap(isValid => {
				if (!isValid) {
					deleteToken();
					this.isLoggedInSubject.next(false);
				}
			}));
	}

	public isTokenValid(token: string): Observable<boolean> {
		return this.apiService
			.post<boolean, UserIsTokenValidDto>(`${this.route}/is-token-valid`, { token });
	}

	public ban(id: string): Observable<void> {
		return this.apiService.post<void>(`${this.route}/${id}/ban`);
	}

	public removeBan(id: string): Observable<void> {
		return this.apiService.post<void>(`${this.route}/${id}/remove-ban`);
	}

	public getAll(): Observable<User[]> {
		return this.apiService.get<User[]>(`${this.route}`).pipe(map(users => users.map(deserializeUser)));
	}

	public getMe(): Observable<User> {
		return this.apiService.get<User>(`${this.route}/me`).pipe(
			map(deserializeUser),
			tap((user: User) => this.currentUserSubject.next(user))
		);
	}

	public getById(id: string): Observable<User> {
		return this.apiService.get<User>(`${this.route}/${id}`).pipe(map(deserializeUser));
	}

	public getPosts(id: string): Observable<Post[]> {
		return this.apiService.get<Post[]>(`${this.route}/${id}/posts`);
	}

	public update(id: string, dto: UserUpdateDto): Observable<User> {
		return this.apiService
			.patch<User, UserUpdateDto>(`${this.route}/${id}`, dto)
			.pipe(map(deserializeUser));
	}

	public delete(id: string): Observable<User> {
		return this.apiService.delete<User>(`${this.route}/${id}`).pipe(map(deserializeUser));
	}
}
