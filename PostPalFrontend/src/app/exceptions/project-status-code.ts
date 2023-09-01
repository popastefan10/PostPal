// Keep in sync with backend implementation
export enum ProjectStatusCode {
	Http400BadRequest,
	Http401Unauthorized,
	Http403Forbidden,
	Http404NotFound,
	Http500InternalServerError,
	UserBanned,
	UserAlreadyHasProfile,
	UnknownError
}
