import { Post } from '../../interfaces/post';
import { UserProfile } from '../../interfaces/user-profile';

export interface PostsWithProfilesDto {
	posts: Post[];
	profiles: { [key: string]: UserProfile; };
}
