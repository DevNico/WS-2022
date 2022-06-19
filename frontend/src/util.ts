import { atom } from 'recoil';

export function dateValueFormatter(params: any) {
	return new Date(params.value).toLocaleDateString();
}

export const isSuperAdminState = atom({
	key: 'superAdminState',
	default: false,
});
