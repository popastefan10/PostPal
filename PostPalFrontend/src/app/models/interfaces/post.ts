import { BaseEntity } from './base-entity';

export interface Post extends BaseEntity {
	userId: string;
	description: string;
	imagesUrls: string[];
}
