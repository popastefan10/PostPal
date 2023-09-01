import { animate, style, transition, trigger } from '@angular/animations';

const beforeSlideTopDownStyle = { opacity: 0, transform: 'translate(-50%, -100%)' };
const afterSlideTopDownStyle = { opacity: 1, transform: '*' };
const afterFadeAwayStyle = { opacity: 0 };

export const slideTopDownFadeAway = trigger('slideTopDownFadeAway', [
	transition(':enter', [style(beforeSlideTopDownStyle), animate('250ms', style(afterSlideTopDownStyle))]),
	transition(':leave', [style(afterSlideTopDownStyle), animate('250ms', style(afterFadeAwayStyle))])
]);
