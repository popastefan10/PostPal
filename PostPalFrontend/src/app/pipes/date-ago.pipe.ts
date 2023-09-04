import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
	name: 'dateAgo'
})
export class DateAgoPipe implements PipeTransform {
	transform(value: string | undefined): string | undefined {
		if (!value) {
			return undefined;
		}

		const date = new Date(value);
		const now = new Date();
		const diffInSeconds = Math.floor((now.getTime() - date.getTime()) / 1000);

		if (diffInSeconds < 60) {
			return `${diffInSeconds} s ago`;
		} else if (diffInSeconds < 3600) {
			const minutes = Math.floor(diffInSeconds / 60);
			return `${minutes} m ago`;
		} else if (diffInSeconds < 86400) {
			const hours = Math.floor(diffInSeconds / 3600);
			return `${hours} h ago`;
		} else {
			const days = Math.floor(diffInSeconds / 86400);
			return `${days} d ago`;
		}
	}
}
