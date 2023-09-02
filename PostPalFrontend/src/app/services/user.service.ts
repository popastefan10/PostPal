import { Injectable } from '@angular/core';
import { tap, Observable, map, BehaviorSubject, takeUntil, of } from 'rxjs';
import { UserAuthRequestDto } from '../models/dtos/user/user-auth-request.dto';
import { UserAuthResponseDto } from '../models/dtos/user/user-auth-response.dto';
import { UserRegisterRequestDto } from '../models/dtos/user/user-register-request.dto';
import { UserRegisterResponseDto } from '../models/dtos/user/user-register-response.dto';
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

	private readonly isLoggedInSubject = new BehaviorSubject<boolean>(getToken() !== null);
	public readonly isLoggedIn$ = this.isLoggedInSubject.asObservable();

	private readonly currentUserSubject = new BehaviorSubject<User | null>(null);
	public readonly currentUser$ = this.currentUserSubject.asObservable();

	constructor(private readonly apiService: ApiService) {
		super();

		this.isLoggedIn$.pipe(takeUntil(this.destroyed$)).subscribe(isLoggedIn => {
			if (isLoggedIn) {
				this.getMe().subscribe();
			} else {
				this.currentUserSubject.next(null);
			}
		});
	}

	public register(dto: UserRegisterRequestDto): Observable<UserRegisterResponseDto> {
		return this.apiService.post<UserRegisterResponseDto>(`${this.route}/register`, dto).pipe(
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

	public isMyTokenValid(): Observable<boolean> {
		const myToken = getToken();
		if (myToken === null) {
			return of(false);
		}

		return this.apiService.post<boolean>(`${this.route}/is-token-valid`, { token: myToken })
			.pipe(tap(isValid => {
				if (!isValid) {
					deleteToken();
					this.isLoggedInSubject.next(false);
				}
			}));
	}

	public logout(): void {
		deleteToken();
		this.isLoggedInSubject.next(false);
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
}
