import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MaterialModule } from './shared/material.module';
import { SharedModule } from './shared/shared.module';
import { NavbarComponent } from './components/navbar/navbar.component';
import { AppRoutingModule } from './app-routing.module';
import { LoginComponent } from './components/login/login.component';
import { JwtModule } from '@auth0/angular-jwt';
import { getToken } from './utils/token.util';
import { RegisterComponent } from './components/register/register.component';

@NgModule({
	declarations: [
		AppComponent,
		NavbarComponent,
		LoginComponent,
		RegisterComponent,
	],
	imports: [
		BrowserModule, HttpClientModule, BrowserAnimationsModule, AppRoutingModule, MaterialModule, SharedModule, JwtModule.forRoot({
			config: {
				tokenGetter: getToken,
				allowedDomains: ['localhost:1200', 'localhost:5071', 'localhost:7121', 'localhost:23792', 'localhost:44410', 'https://localhost:44410']
			}
		})
	],
	providers: [],
	bootstrap: [AppComponent]
})
export class AppModule { }
