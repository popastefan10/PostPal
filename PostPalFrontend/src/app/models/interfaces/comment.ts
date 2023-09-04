import { BaseEntity } from './base-entity';

export interface Comment extends BaseEntity {
	userId: string;
	postId: string;
	content: string;
}
