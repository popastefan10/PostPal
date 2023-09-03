import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { FormControlErrorMessagePipe } from '../pipes/form-control-error-message.pipe';

const PIPES = [FormControlErrorMessagePipe];

@NgModule({
	exports: [
		RouterModule,
		ReactiveFormsModule,
		FormsModule,
		...PIPES,
	],
	declarations: [...PIPES],
})
export class SharedModule { }
