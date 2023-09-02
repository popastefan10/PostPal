import { Directive, OnDestroy } from '@angular/core';
import { Subject } from 'rxjs';

@Directive()
export class SubscriptionCleanup implements OnDestroy {
	private readonly destroyedSubject = new Subject<void>();
	protected readonly destroyed$ = this.destroyedSubject.asObservable();

	ngOnDestroy(): void {
		this.destroyedSubject.next();
	}
}
