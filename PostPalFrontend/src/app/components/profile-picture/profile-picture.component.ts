import { Component, HostBinding, Input, OnInit } from '@angular/core';
import { RoutePaths } from '../../app-routing.module';
import { UserProfile } from '../../models/interfaces/user-profile';

@Component({
	selector: 'pal-profile-picture',
	templateUrl: './profile-picture.component.html',
	styleUrls: ['./profile-picture.component.scss']
})
export class ProfilePictureComponent implements OnInit {
	@Input() public profile: UserProfile | undefined;
	@Input() public sizeInPixels: number = 32;
	@HostBinding('style.--picture-size') get pictureSizeInPixels(): string {
		return this.sizeInPixels + 'px';
	}
	public readonly profilePath = RoutePaths.profile;

	constructor() { }

	ngOnInit(): void { }
}
