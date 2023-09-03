import { Role } from '../../enums/role';

export interface UserUpdateDto {
	email: string | null;
	role: Role | null;
}
