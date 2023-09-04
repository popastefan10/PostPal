import { Comment } from '../../interfaces/comment';
import { UserProfile } from '../../interfaces/user-profile';

export interface CommentWithProfileDto {
	comment: Comment;
	profile?: UserProfile | null;
}
