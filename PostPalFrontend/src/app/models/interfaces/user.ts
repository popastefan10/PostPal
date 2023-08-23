import { Role } from '../enums/role';
import { BaseEntity } from './base-entity';

export interface User extends BaseEntity {
	email: string;
	role: Role;
	isBanned?: boolean;
}
