import { Component, Input, OnInit } from '@angular/core';
import { map, merge, Observable, startWith, Subject, tap, scan, throttleTime } from 'rxjs';
import { Post } from '../../models/interfaces/post';

enum ImageEvent {
	Previous,
	Next
}

@Component({
	selector: 'pal-post-images',
	templateUrl: './post-images.component.html',
	styleUrls: ['./post-images.component.scss']
})
export class PostImagesComponent implements OnInit {
	@Input() post!: Post;

	//private readonly currentIndexSubject = new BehaviorSubject<number>(0);
	//public readonly currentIndex$ = this.currentIndexSubject.asObservable();

	private readonly previousSubject: Subject<void> = new Subject<void>();
	public readonly previous$ = this.previousSubject.asObservable();
	private readonly nextSubject: Subject<void> = new Subject<void>();
	public readonly next$ = this.nextSubject.asObservable();

	public readonly events: Observable<ImageEvent> = merge(
		this.previous$.pipe(map(() => ImageEvent.Previous)),
		this.next$.pipe(map(() => ImageEvent.Next))
	);

	public readonly currentIndex$: Observable<number> = this.events.pipe(
		startWith(ImageEvent.Previous),
		throttleTime(250),
		scan((currentIndex: number, event: ImageEvent) => {
			if (event === ImageEvent.Previous) {
				return Math.max(currentIndex - 1, 0);
			} else if (event === ImageEvent.Next) {
				return Math.min(currentIndex + 1, this.post?.imagesUrls.length - 1);
			}
			return currentIndex;
		}, 0)
	);

	public readonly previousVisible$: Observable<boolean> = this.currentIndex$.pipe(
		map((currentIndex: number) => currentIndex > 0)
	);
	public readonly nextVisible$: Observable<boolean> = this.currentIndex$.pipe(
		map((currentIndex: number) => currentIndex < this.post?.imagesUrls.length - 1)
	);

	constructor() { }

	ngOnInit(): void { }

	public onPreviousPressed(): void {
		this.previousSubject.next();
	}

	public onNextPressed(): void {
		this.nextSubject.next();
	}
}
