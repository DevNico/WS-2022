import Box from '@mui/material/Box/Box';
import styled from '@mui/material/styles/styled';
import React from 'react';
import { Outlet } from 'react-router-dom';
import AppBar from './components/AppBar';

const Main = styled(Box)`
	flex-grow: 1;
	align-items: stretch;
	display: flex;
	flex-basis: auto;
	flex-direction: row;
	height: 100%;
`;

const Content = styled(Box)`
	flex-grow: 1;
	overflow: auto;
	width: 100%;
	height: calc(100% - ${({ theme }) => theme.spacing(8)});

	[${({ theme }) => theme.breakpoints.down('sm')}] {
		height: calc(100% - ${({ theme }) => theme.spacing(4)});
	}
`;

interface BaseLayoutProps {
	drawer?: React.ReactNode;
}

const BaseLayout: React.FC<BaseLayoutProps> = ({ drawer }) => {
	return (
		<>
			<AppBar hasDrawer={drawer !== undefined} />

			<Main>
				{drawer}
				<Content p={[2, 4]}>
					<Outlet />
				</Content>
			</Main>
		</>
	);
};

export default BaseLayout;
