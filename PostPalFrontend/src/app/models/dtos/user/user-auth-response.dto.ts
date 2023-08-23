import { Role } from '../../enums/role';

export interface UserAuthResponseDto {
	id: string;
	email: string;
	role: Role;
	token: string;
}
