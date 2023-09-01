import { BackendException } from '../exceptions/project-exception';

export function isProjectException(error: any): asserts error is BackendException {
	if (!error || (!error.Code && error.Code !== 0) || !error.Details || !error.CodeName) {
		throw new Error('Invalid error object');
	}
};

export const getErrorMessage = (error: any): string => {
	try {
		isProjectException(error);
		return error.Details;
	} catch (e) {
		return error?.message || error?.error?.message || 'Something went wrong';
	}
};
