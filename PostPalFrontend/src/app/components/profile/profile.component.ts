import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { catchError, filter, map, Observable, of, startWith, switchMap } from 'rxjs';
import { UserProfile } from '../../models/interfaces/user-profile';
import { ProfileService } from '../../services/profile.service';

@Component({
	selector: 'pal-profile',
	templateUrl: './profile.component.html',
	styleUrls: ['./profile.component.scss']
})
export class ProfileComponent implements OnInit {
	public readonly id$: Observable<string | undefined | null> =
		this.route.params.pipe(
			startWith(undefined),
			map(params => params === undefined ? undefined : (params['id'] || null)));

	public readonly isMyProfile$: Observable<boolean | undefined> = this.id$.pipe(map(id => id === undefined ? undefined : (id === null)));

	public readonly profile$: Observable<UserProfile | null> =
		this.id$
			.pipe(
				filter(id => id !== undefined),
				switchMap(
					id => id === null
						? this.profileService.getMe()
						: this.profileService.getById(id!).pipe(catchError((error) => of(null)))
				),
			);

	constructor(private readonly route: ActivatedRoute, private readonly profileService: ProfileService) { }

	ngOnInit(): void { }
}
