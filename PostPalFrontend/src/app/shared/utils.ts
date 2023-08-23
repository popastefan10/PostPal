export function getBaseUrl(): string {
	return document.getElementsByTagName('base')[0].href;
}

export function setToken(token: string): void {
	localStorage.setItem('token', token);
}

export function getToken(): string | null {
	return localStorage.getItem('token');
}
