export interface ProfileCreateDto {
	firstName: string;
	lastName: string;
	bio?: string | null;
	profilePicture?: File | null;
}
