import { Role } from '../../enums/role';

export interface UserRegisterResponseDto {
	id: string;
	email: string;
	role: Role;
	token: string;
}
