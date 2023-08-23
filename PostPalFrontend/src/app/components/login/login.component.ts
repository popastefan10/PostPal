import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { tap } from 'rxjs';
import { routePaths } from '../../app-routing.module';
import { UserAuthRequestDto } from '../../models/dtos/user/user-auth-request.dto';
import { UserService } from '../../services/user.service';

@Component({
	selector: 'pal-login',
	templateUrl: './login.component.html',
	styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
	public readonly loginForm = this.formBuilder.group({
		email: ['', [Validators.required, Validators.email]],
		password: ['', [Validators.required]]
	});

	public isPasswordVisible: boolean = false;

	constructor(private readonly formBuilder: FormBuilder, private readonly userService: UserService, private readonly router: Router) { }

	public ngOnInit(): void { }

	public onSubmit(): void {
		const dto: UserAuthRequestDto = {
			email: this.email.value,
			password: this.password.value
		};
		this.userService.login(dto).pipe(tap(() => this.router.navigateByUrl(routePaths.home))).subscribe();
	}

	public get email(): FormControl {
		return this.loginForm.controls.email;
	}

	public get password(): FormControl {
		return this.loginForm.controls.password;
	}

	public togglePasswordVisibility(): void {
		this.isPasswordVisible = !this.isPasswordVisible;
	}
}
