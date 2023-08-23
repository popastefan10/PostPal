import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { UserService } from './services/user.service';

@Component({
	selector: 'app-root',
	templateUrl: './app.component.html',
	styleUrls: ['./app.component.scss']
})
export class AppComponent {
	constructor(http: HttpClient, private readonly userService: UserService) { }

	title = 'PostPalFrontend';

	public getAllUsers(): void {
		this.userService.getAll().subscribe(console.log);
	}
}
