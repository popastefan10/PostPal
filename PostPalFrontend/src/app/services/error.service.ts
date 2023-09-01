import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { BackendException } from '../exceptions/project-exception';
import { ProjectStatusCode } from '../exceptions/project-status-code';
import { getErrorMessage, isProjectException } from '../utils/error.util';

@Injectable({
	providedIn: 'root'
})
export class ErrorService {
	private readonly errorsSubject = new BehaviorSubject<BackendException[]>([]);
	public readonly errors$ = this.errorsSubject.asObservable();

	constructor() {
		this.errors$.subscribe(console.log);
	}

	private addProjectException(error: BackendException): void {
		const errors = this.errorsSubject.value;
		this.errorsSubject.next([...errors, error]);
	}

	private tryAddProjectException(error: any): boolean {
		try {
			isProjectException(error);
			this.addProjectException(error);
			return true;
		} catch (e) {
			return false;
		}
	}

	public handleError(error: any): void {
		if (this.tryAddProjectException(error?.error)) {
			return;
		}
		if (this.tryAddProjectException(error)) {
			return;
		}

		const unknownError: BackendException = {
			Code: ProjectStatusCode.UnknownError,
			Details: getErrorMessage(error),
			CodeName: 'UnknownError',
		};
		this.addProjectException(unknownError);
	}

	public closeError(index: number): void {
		const errors = this.errorsSubject.value;
		errors.splice(index, 1);
		this.errorsSubject.next(errors);
	}
}
