import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { catchError, tap } from 'rxjs';
import { ErrorService } from '../../services/error.service';
import { ProfileService } from '../../services/profile.service';

@Component({
	selector: 'pal-create-profile',
	templateUrl: './create-profile.component.html',
	styleUrls: ['./create-profile.component.scss']
})
export class CreateProfileComponent implements OnInit {
	public readonly createProfileForm = this.formBuilder.group({
		firstName: ['', Validators.required],
		lastName: ['', Validators.required],
		bio: [''],
		profilePicture: [null] as [File | null]
	});
	public profilePictureUrl: string | null = null;

	constructor(
		private readonly router: Router,
		private readonly formBuilder: FormBuilder,
		private readonly profileService: ProfileService,
		private readonly errorService: ErrorService
	) { }

	ngOnInit(): void {
	}

	public get firstName() {
		return this.createProfileForm.controls.firstName;
	}

	public get lastName() {
		return this.createProfileForm.controls.lastName;
	}

	public get bio() {
		return this.createProfileForm.controls.bio;
	}

	public get profilePicture() {
		return this.createProfileForm.controls.profilePicture;
	}

	public onFileSelect(event: Event): void {
		const target = event.target as HTMLInputElement;
		if (target && target.files && target.files.length) {
			const file = target.files[0];
			this.createProfileForm.patchValue({ profilePicture: file });
			target.value = '';

			const reader = new FileReader();
			reader.readAsDataURL(file);
			reader.onload = () => {
				this.profilePictureUrl = reader.result as string;
			};
		} else {
			console.error('Something went wrong with the file upload!');
		}
	}

	public onSubmit(): void {
		this.profileService
			.create({
				firstName: this.firstName.value || '',
				lastName: this.lastName.value || '',
				bio: this.bio.value,
				profilePicture: this.profilePicture.value
			})
			.pipe(
				tap(() => this.router.navigateByUrl('')),
				catchError(error => {
					this.errorService.handleError(error);
					throw error;
				})).subscribe();
	}
}
