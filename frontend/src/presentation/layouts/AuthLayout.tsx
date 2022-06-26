import { styled } from '@mui/material/styles';
import { useKeycloak } from '@react-keycloak/web';
import React, { useEffect } from 'react';
import Div100vh from 'react-div-100vh';
import { Outlet } from 'react-router-dom';
import { useSetRecoilState } from 'recoil';
import FullScreenLoading from '../components/layout/FullScreenLoading';
import { isSuperAdminState } from '../store/keycloakState';

const Root = styled(Div100vh)`
	display: flex;
	flex-direction: column;
	flex: 0% 1 1;
	overflow: hidden;
`;

const AuthLayout: React.FC = () => {
	const { initialized, keycloak } = useKeycloak();
	const setIsSuperAdmin = useSetRecoilState(isSuperAdminState);

	useEffect(() => {
		if (keycloak.authenticated) {
			keycloak
				.loadUserInfo()
				.then(() => {
					setIsSuperAdmin(keycloak.hasRealmRole('superAdmin'));
				})
				.catch(() => {
					setIsSuperAdmin(false);
				});
		} else {
			setIsSuperAdmin(false);
		}
	}, [keycloak.authenticated]);

	return <Root>{(!initialized && <FullScreenLoading />) || <Outlet />}</Root>;
};

export default AuthLayout;
