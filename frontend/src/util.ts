import Keycloak from 'keycloak-js';
import { NavigateFunction } from 'react-router-dom';

export function checkUserIsSuperAdminEffect(
	keycloak: Keycloak,
	navigate: NavigateFunction,
	setLoading: (loading: boolean) => void,
	mutation: () => Promise<any>
) {
	if (!keycloak.authenticated) {
		navigate('/unauthorized');
		return;
	}

	keycloak.loadUserInfo().then(async () => {
		if (!keycloak.hasRealmRole('superAdmin')) {
			navigate('/unauthorized');
			return;
		}

		await mutation();
		setLoading(false);
	});
}

export function dateValueFormatter(params: any) {
	return new Date(params.value).toLocaleDateString();
}
