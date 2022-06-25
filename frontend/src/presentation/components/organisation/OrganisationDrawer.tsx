import { CorporateFare } from '@mui/icons-material';
import Box from '@mui/material/Box/Box';
import MuiDrawer from '@mui/material/Drawer';
import List from '@mui/material/List/List';
import ListSubheader from '@mui/material/ListSubheader/ListSubheader';
import React from 'react';
import { useTranslation } from 'react-i18next';
import { useRecoilState } from 'recoil';
import { useRouteParams } from 'typesafe-routes';
import { useOrganisationsGetByName } from '../../../api/organisation/organisation';
import { homeRoute, organisationRoute } from '../../Router';
import { drawerOpenState } from '../../store/generalState';
import ListItemLink from '../ListItemLink';

export const drawerWidth = 240;

const OrganisationDrawer: React.FC = () => {
	const { t } = useTranslation();
	const [drawerOpen, setDrawerOpen] = useRecoilState(drawerOpenState);

	const { name } = useRouteParams(organisationRoute);
	const organisation = useOrganisationsGetByName(name);

	const handleDrawerToggle = () => {
		setDrawerOpen((v) => !v);
	};

	const drawer = (
		<List
			subheader={
				<ListSubheader>{organisation?.data?.name}</ListSubheader>
			}
		>
			<ListItemLink
				to={homeRoute({}).organisation({ name }).users({})}
				text={t('organisation.drawer.users')}
				icon={<CorporateFare />}
				onClick={handleDrawerToggle}
			/>
			<ListItemLink
				to={homeRoute({}).organisation({ name }).roles({})}
				text={t('organisation.drawer.roles')}
				icon={<CorporateFare />}
				onClick={handleDrawerToggle}
			/>
		</List>
	);

	return (
		<Box
			component='nav'
			sx={{
				display: 'flex',
				width: { sm: drawerWidth },
				flexShrink: { sm: 0 },
			}}
			aria-label='mailbox folders'
		>
			{/* The implementation can be swapped with js to avoid SEO duplication of links. */}
			<MuiDrawer
				variant='temporary'
				open={drawerOpen}
				onClose={handleDrawerToggle}
				ModalProps={{
					keepMounted: true, // Better open performance on mobile.
				}}
				sx={{
					display: { xs: 'block', sm: 'none' },
					flexGrow: 1,
					'& .MuiDrawer-paper': {
						boxSizing: 'border-box',
						width: drawerWidth,
						position: 'relative',
					},
				}}
			>
				{drawer}
			</MuiDrawer>
			<MuiDrawer
				variant='permanent'
				sx={{
					display: { xs: 'none', sm: 'block' },
					flexGrow: 1,
					'& .MuiDrawer-paper': {
						boxSizing: 'border-box',
						width: drawerWidth,
						position: 'relative',
					},
				}}
				open
			>
				{drawer}
			</MuiDrawer>
		</Box>
	);
};

export default OrganisationDrawer;
