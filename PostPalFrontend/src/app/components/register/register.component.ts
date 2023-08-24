import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { tap } from 'rxjs';
import { RoutePaths } from '../../app-routing.module';
import { UserRegisterRequestDto } from '../../models/dtos/user/user-register-request.dto';
import { UserService } from '../../services/user.service';

@Component({
	selector: 'pal-register',
	templateUrl: './register.component.html',
	styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {
	public readonly registerForm = this.formBuilder.group({
		email: ['', [Validators.required, Validators.email]],
		password: ['', [Validators.required]]
	});

	public isPasswordVisible: boolean = false;

	constructor(
		private readonly router: Router,
		private readonly formBuilder: FormBuilder,
		private readonly userService: UserService
	) { }

	ngOnInit(): void {
	}

	public onSubmit(): void {
		const dto: UserRegisterRequestDto = {
			email: this.email.value,
			password: this.password.value
		};
		this.userService.register(dto).pipe(tap(() => this.router.navigateByUrl(RoutePaths.home))).subscribe();
	}

	public get email(): FormControl {
		return this.registerForm.controls.email;
	}

	public get password(): FormControl {
		return this.registerForm.controls.password;
	}

	public togglePasswordVisibility(): void {
		this.isPasswordVisible = !this.isPasswordVisible;
	}
}
