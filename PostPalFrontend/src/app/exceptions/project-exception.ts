import { ProjectStatusCode } from './project-status-code';

export interface BackendException {
	Code: ProjectStatusCode;
	CodeName: string;
	Details: string;
}
