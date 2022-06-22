import { atom } from 'recoil';

export const isSuperAdminState = atom<boolean>({
	key: 'isSuperAdmin',
	default: false,
});
