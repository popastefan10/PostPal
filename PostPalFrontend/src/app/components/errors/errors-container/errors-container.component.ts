import { Component, OnInit } from '@angular/core';
import { ErrorService } from '../../../services/error.service';
import { slideTopDownFadeAway } from '../../../utils/animations';

@Component({
	selector: 'pal-errors-container',
	templateUrl: './errors-container.component.html',
	styleUrls: ['./errors-container.component.scss'],
	animations: [slideTopDownFadeAway]
})
export class ErrorsContainerComponent implements OnInit {
	public errors$ = this.errorService.errors$;

	constructor(private readonly errorService: ErrorService) { }

	public ngOnInit(): void {
	}

	public closeError(index: number): void {
		this.errorService.closeError(index);
	}
}
