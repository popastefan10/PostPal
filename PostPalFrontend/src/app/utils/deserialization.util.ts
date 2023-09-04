import { BaseEntity } from '../models/interfaces/base-entity';
import { User } from '../models/interfaces/user';

const deserializeBaseEntity = (entity: BaseEntity): BaseEntity => {
	const { id, dateCreated, dateModified, ...rest } = entity;

	return {
		id,
		dateCreated,
		dateModified,
		...rest
	};
};

export const deserializeUser = (user: User): User => {
	return deserializeBaseEntity(user) as User;
}
