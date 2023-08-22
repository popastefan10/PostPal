import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { MatButtonModule } from '@angular/material/button';

const MATERIAL_MODULES = [
	MatSlideToggleModule,
	MatButtonModule,
];

@NgModule({
	declarations: [],
	imports: [
		CommonModule
	],
	exports: [...MATERIAL_MODULES]
})
export class MaterialModule { }
