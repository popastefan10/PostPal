import { Component, Input, OnInit } from '@angular/core';
import { RoutePaths } from '../../app-routing.module';
import { UserProfile } from '../../models/interfaces/user-profile';

@Component({
  selector: 'pal-profile-name',
  templateUrl: './profile-name.component.html',
  styleUrls: ['./profile-name.component.scss']
})
export class ProfileNameComponent implements OnInit {
	@Input() profile!: UserProfile;
	public get profileName(): string {
		return this.profile.firstName + ' ' + this.profile.lastName;
	}
	public get profilePath(): string {
		return '/' + RoutePaths.profile + '/' + this.profile.id;
	}

  constructor() { }

  ngOnInit(): void {
  }
}
