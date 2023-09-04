import { Comment } from '../../interfaces/comment';
import { UserProfile } from '../../interfaces/user-profile';

export interface CommentsWithProfilesDto {
	comments: Comment[];
	profiles: { [key: string]: UserProfile; };
}
