import { Injectable } from '@angular/core';
import { BehaviorSubject, map, Observable, takeUntil, tap } from 'rxjs';
import { ProfileCreateDto } from '../models/dtos/profile/profile-create.dto';
import { ProfileUpdateDto } from '../models/dtos/profile/profile-update.dto';
import { GetProfilesQueryType } from '../models/enums/get-profiles-query-type';
import { UserProfile } from '../models/interfaces/user-profile';
import { SubscriptionCleanup } from '../utils/subscription-cleanup';
import { ApiService } from './api.service';
import { UserService } from './user.service';

@Injectable({
	providedIn: 'root'
})
export class ProfileService extends SubscriptionCleanup {
	private readonly route = 'profiles';

	private readonly currentProfileSubject = new BehaviorSubject<UserProfile | null>(null);
	public readonly currentProfile$ = this.currentProfileSubject.asObservable();

	constructor(private readonly apiService: ApiService, private readonly userService: UserService) {
		super();

		this.userService.currentUser$.pipe(takeUntil(this.destroyed$)).subscribe(user => {
			if (user) {
				this.getMe().subscribe();
			} else {
				this.currentProfileSubject.next(null);
			}
		});
	}

	public get currentProfileExists(): boolean {
		return !!this.currentProfileSubject.value;
	}

	public create(dto: ProfileCreateDto): Observable<UserProfile> {
		const formData = new FormData();
		formData.append('firstName', dto.firstName);
		formData.append('lastName', dto.lastName);
		if (dto.bio) {
			formData.append('bio', dto.bio);
		}
		if (dto.profilePicture) {
			formData.append('profilePicture', dto.profilePicture);
		}

		return this.apiService.post<UserProfile>(this.route, formData).pipe(tap(profile => this.currentProfileSubject.next(profile)));
	}

	public hasProfile(): Observable<boolean> {
		return this.apiService.post<boolean>(`${this.route}/has-profile`);
	}

	public getAll(): Observable<UserProfile[]> {
		return this.apiService.get<UserProfile[]>(`${this.route}`);
	}

	public getMe(): Observable<UserProfile> {
		return this.apiService.get<UserProfile>(`${this.route}/me`).pipe(tap(profile => this.currentProfileSubject.next(profile)));
	}

	public getById(id: string): Observable<UserProfile> {
		return this.apiService.get<UserProfile>(`${this.route}/${id}?query-type=${GetProfilesQueryType.byId}`);
	}

	public getByIds(ids: string[]): Observable<UserProfile[]> {
		const idsString = ids.join(',');

		return this.apiService.get<UserProfile[]>(`${this.route}/${idsString}?query-type=${GetProfilesQueryType.byIds}`);
	}

	public getByUserId(userId: string): Observable<UserProfile> {
		return this.apiService.get<UserProfile>(`${this.route}/${userId}?query-type=${GetProfilesQueryType.byUserId}`);
	}

	public update(dto: ProfileUpdateDto): Observable<UserProfile> {
		const formData = new FormData();
		if (dto.firstName) {
			formData.append('firstName', dto.firstName);
		}
		if (dto.lastName) {
			formData.append('lastName', dto.lastName);
		}
		if (dto.bio) {
			formData.append('bio', dto.bio);
		}
		if (dto.profilePicture) {
			formData.append('profilePicture', dto.profilePicture);
		}

		return this.apiService
			.patch<UserProfile>(`${this.route}`, formData)
			.pipe(tap(profile => this.currentProfileSubject.next(profile)));
	}

	public deleteMe(): Observable<UserProfile> {
		return this.apiService
			.delete<UserProfile>(`${this.route}/me`)
			.pipe(tap(() => this.currentProfileSubject.next(null)));
	}
}
