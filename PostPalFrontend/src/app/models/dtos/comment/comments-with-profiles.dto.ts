import { Comment } from '../../interfaces/comment';
import { UserProfile } from '../../interfaces/user-profile';

export type ProfilesDictionary = { [key: string]: UserProfile; };

export interface CommentsWithProfilesDto {
	comments: Comment[];
	profiles: ProfilesDictionary;
}
