import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { BehaviorSubject, catchError, map, tap } from 'rxjs';
import { ErrorService } from '../../services/error.service';
import { PostService } from '../../services/post.service';

@Component({
	selector: 'pal-create-post',
	templateUrl: './create-post.component.html',
	styleUrls: ['./create-post.component.scss']
})
export class CreatePostComponent implements OnInit {
	private readonly uploadLimit = 10;

	public readonly createPostForm = this.formBuilder.group({
		description: ['', Validators.required],
		images: [[] as File[], [Validators.minLength(1), Validators.maxLength(10)]]
	});

	private readonly uploadedImagesSubject: BehaviorSubject<File[]> = new BehaviorSubject<File[]>([]);
	public readonly uploadedImages$ = this.uploadedImagesSubject.asObservable();

	public readonly uploadedImagesCount$ = this.uploadedImages$.pipe(map(images => images.length));

	private readonly uploadedImagesUrlsSubject: BehaviorSubject<string[]> = new BehaviorSubject<string[]>([]);
	public readonly uploadedImagesUrls$ = this.uploadedImagesUrlsSubject.asObservable();

	public readonly canUpload$ = this.uploadedImagesCount$.pipe(map(count => count < this.uploadLimit));

	constructor(
		private readonly router: Router,
		private readonly formBuilder: FormBuilder,
		private readonly postService: PostService,
		private readonly errorService: ErrorService
	) { }

	ngOnInit(): void {
		this.uploadedImages$.pipe(tap(images => this.createPostForm.patchValue({ images }))).subscribe();
	}

	public get description() {
		return this.createPostForm.controls.description;
	}

	public get images() {
		return this.createPostForm.controls.images;
	}

	private addUploadedImage(image: File): void {
		const uploadedImages = this.uploadedImagesSubject.value;
		if (uploadedImages.length < this.uploadLimit) {
			uploadedImages.push(image);
		}

		this.uploadedImagesSubject.next(uploadedImages);
	}

	private addUploadedImageUrl(url: string): void {
		const uploadedImagesUrls = this.uploadedImagesUrlsSubject.value;
		if (uploadedImagesUrls.length < this.uploadLimit) {
			uploadedImagesUrls.push(url);
		}

		this.uploadedImagesUrlsSubject.next(uploadedImagesUrls);
	}

	public onFileSelect(event: Event): void {
		const target = event.target as HTMLInputElement;
		if (target && target.files && target.files.length) {
			const file = target.files[0];
			this.addUploadedImage(file);
			target.value = '';

			const reader = new FileReader();
			reader.readAsDataURL(file);
			reader.onload = () => {
				this.addUploadedImageUrl(reader.result as string);
			};
		}
		else {
			console.error('Something went wrong with the file upload!');
		}
	}

	public removeImage(index: number): void {
		const uploadedImages = this.uploadedImagesSubject.value;
		uploadedImages.splice(index, 1);
		this.uploadedImagesSubject.next(uploadedImages);

		const uploadedImagesUrls = this.uploadedImagesUrlsSubject.value;
		uploadedImagesUrls.splice(index, 1);
		this.uploadedImagesUrlsSubject.next(uploadedImagesUrls);
	}

	public onSubmit(): void {
		this.postService
			.create({
				description: this.description.value!,
				images: this.images.value!
			})
			.pipe(
				tap(() => this.router.navigateByUrl('')), // TODO navigate to post page
				catchError(error => {
					this.errorService.handleError(error);
					throw error;
				})
			)
			.subscribe();
	}
}
