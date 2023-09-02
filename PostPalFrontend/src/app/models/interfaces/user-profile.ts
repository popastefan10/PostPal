import { BaseEntity } from './base-entity';

export interface UserProfile extends BaseEntity {
	userId: string;
	firstName: string;
	lastName: string;
	profilePictureUrl: string | null;
	bio: string | null;
}
