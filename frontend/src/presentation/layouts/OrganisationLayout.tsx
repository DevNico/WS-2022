import Box from '@mui/material/Box/Box';
import { styled } from '@mui/material/styles';
import { createSpacing } from '@mui/system';
import React from 'react';
import { Navigate, Outlet } from 'react-router-dom';
import { useRouteParams } from 'typesafe-routes';
import { useOrganisationsGetByName } from '../../api/organisation/organisation';
import OrganisationDrawer from '../components/organisation/OrganisationDrawer';
import QueryWrapper from '../components/QueryWrapper';
import { organisationRoute } from '../Router';

const Root = styled(Box)`
	flex-grow: 1;
	align-items: stretch;
	display: flex;
	flex-basis: auto;
	flex-direction: row;
	height: calc(100% + ${({ theme }) => theme.spacing(8)});

	${({ theme }) => theme.breakpoints.down('xs')} {
		height: calc(100% + ${({ theme }) => theme.spacing(4)});
	}
`;

const Content = styled(Box)`
	flex-grow: 1;
	overflow: auto;
	width: 100%;
`;

const OrganisationLayout: React.FC = () => {
	const { name } = useRouteParams(organisationRoute);
	const organisation = useOrganisationsGetByName(name);

	return (
		<QueryWrapper result={organisation} error={<Navigate to={'/404'} />}>
			{() => (
				<Root m={[-2, -4]}>
					<OrganisationDrawer />
					<Content p={[2, 4]}>
						<Outlet />
					</Content>
				</Root>
			)}
		</QueryWrapper>
	);
};

export default OrganisationLayout;
