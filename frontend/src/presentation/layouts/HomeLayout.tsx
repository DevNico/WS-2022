import Box from '@mui/material/Box/Box';
import styled from '@mui/material/styles/styled';
import { useKeycloak } from '@react-keycloak/web';
import React, { useEffect } from 'react';
import Div100vh from 'react-div-100vh';
import { Outlet, useMatch } from 'react-router-dom';
import { useSetRecoilState } from 'recoil';
import AppBar from '../components/layout/AppBar';
import Drawer from '../components/layout/Drawer';
import FullScreenLoading from '../components/layout/FullScreenLoading';
import { homeRoute } from '../Router';
import { isSuperAdminState } from '../store/keycloakState';

const Root = styled(Div100vh)`
	display: flex;
	flex-direction: column;
	flex: 0% 1 1;
	overflow: hidden;
`;
const Main = styled(Box)`
	flex-grow: 1;
	align-items: stretch;
	display: flex;
	flex-basis: auto;
	flex-direction: row;
`;

const Content = styled(Box)`
	flex-grow: 1;
	overflow: auto;
	width: 100%;
`;

const HomeLayout: React.FC = () => {
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

	return (
		<Root>
			{(!initialized && <FullScreenLoading />) || (
				<>
					<AppBar />

					<Main>
						<Drawer />
						<Content p={[2, 4]}>
							<Outlet />
						</Content>
					</Main>
				</>
			)}
		</Root>
	);
};

export default HomeLayout;
