import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { catchError, tap } from 'rxjs';

@Component({
	selector: 'app-root',
	templateUrl: './app.component.html',
	styleUrls: ['./app.component.scss']
})
export class AppComponent {
	public forecasts?: WeatherForecast[];

	constructor (http: HttpClient) {
		http.get<WeatherForecast[]>('/weatherforecast').pipe(
			tap(result => {
				this.forecasts = result;
			}),
			catchError(error => {
				console.error(error);

				throw error;
			})).subscribe();
	}

	title = 'PostPalFrontend';
}

interface WeatherForecast {
	date: string;
	temperatureC: number;
	temperatureF: number;
	summary: string;
}
