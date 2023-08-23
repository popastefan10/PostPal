import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatCardModule } from '@angular/material/card';
import { MatDividerModule } from '@angular/material/divider';
import { MatIconModule } from '@angular/material/icon';

const MATERIAL_MODULES = [
	MatSlideToggleModule,
	MatButtonModule,
	MatFormFieldModule,
	MatInputModule,
	MatCardModule,
	MatDividerModule,
	MatIconModule
];

@NgModule({
	declarations: [],
	imports: [
		CommonModule
	],
	exports: [...MATERIAL_MODULES]
})
export class MaterialModule { }
