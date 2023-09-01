import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { catchError, Observable } from 'rxjs';
import { ErrorService } from './error.service';

@Injectable({
	providedIn: 'root'
})
export class ApiService {
	private readonly apiUrl: string;

	constructor(
		private readonly httpClient: HttpClient,
		@Inject('BASE_URL') baseUrl: string,
		private readonly errorService: ErrorService
	) {
		this.apiUrl = baseUrl + 'api/';
	}

	get<T>(path: string, params = {}): Observable<T> {
		return this.httpClient
			.get<T>(`${this.apiUrl}${path}`, { params })
			.pipe(catchError(error => {
				this.errorService.handleError(error);
				throw error;
			}));
	}

	put<T>(path: string, body = {}): Observable<T> {
		return this.httpClient
			.put<T>(`${this.apiUrl}${path}`, body)
			.pipe(catchError(error => {
				this.errorService.handleError(error);
				throw error;
			}));
	}

	post<T>(path: string, body = {}): Observable<T> {
		return this.httpClient
			.post<T>(`${this.apiUrl}${path}`, body)
			.pipe(catchError(error => {
				this.errorService.handleError(error);
				throw error;
			}));
	}

	delete<T>(path: string): Observable<T> {
		return this.httpClient
			.delete<T>(`${this.apiUrl}${path}`)
			.pipe(catchError(error => {
				this.errorService.handleError(error);
				throw error;
			}));
	}
}
