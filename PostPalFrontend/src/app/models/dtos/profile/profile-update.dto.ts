export interface ProfileUpdateDto {
	firstName: string | null;
	lastName: string | null;
	bio: string | null;
	profilePicture: File | null;
}
