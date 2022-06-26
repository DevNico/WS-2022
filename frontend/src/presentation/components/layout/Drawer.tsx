import { CorporateFare } from '@mui/icons-material';
import Box from '@mui/material/Box/Box';
import MuiDrawer from '@mui/material/Drawer';
import List from '@mui/material/List/List';
import ListSubheader from '@mui/material/ListSubheader/ListSubheader';
import React from 'react';
import { useTranslation } from 'react-i18next';
import { useMatch } from 'react-router-dom';
import { useRecoilState, useSetRecoilState } from 'recoil';
import {
	useOrganisationsGetById,
	useOrganisationsGetByRouteName,
} from '../../../api/organisation/organisation';
import { useServiceGetByRouteName } from '../../../api/service/service';
import { homeRoute, organisationRoute, serviceRoute } from '../../Router';
import { drawerOpenState } from '../../store/generalState';
import ListItemLink from '../ListItemLink';
import ArrowBackIcon from '@mui/icons-material/ArrowBackIosNew';
import GroupIcon from '@mui/icons-material/Group';
import SecurityIcon from '@mui/icons-material/Security';
import AppsIcon from '@mui/icons-material/Apps';
export const drawerWidth = 240;

interface OrganisationDrawerProps {
	name: string;
	handleDrawerToggle: () => void;
}

const OrganisationDrawer: React.FC<OrganisationDrawerProps> = ({
	handleDrawerToggle,
	name,
}) => {
	const { t } = useTranslation();
	const organisation = useOrganisationsGetByRouteName(name);

	return (
		<List>
			<ListSubheader>{organisation?.data?.name}</ListSubheader>
			<ListItemLink
				to={homeRoute({}).organisation({ name }).services({})}
				text={t('organisation.drawer.services')}
				icon={<AppsIcon />}
				onClick={handleDrawerToggle}
			/>
			<ListItemLink
				to={homeRoute({}).organisation({ name }).users({})}
				text={t('organisation.drawer.users')}
				icon={<GroupIcon />}
				onClick={handleDrawerToggle}
			/>
			<ListItemLink
				to={homeRoute({}).organisation({ name }).roles({})}
				text={t('organisation.drawer.roles')}
				icon={<SecurityIcon />}
				onClick={handleDrawerToggle}
			/>
		</List>
	);
};

interface ServiceDrawerProps {
	serviceRouteName: string;
	handleDrawerToggle: () => void;
}

const ServiceDrawer: React.FC<ServiceDrawerProps> = ({
	serviceRouteName,
	handleDrawerToggle,
}) => {
	const { t } = useTranslation();
	const service = useServiceGetByRouteName(serviceRouteName);
	const organisation = useOrganisationsGetById(
		service.data?.organisationId ?? 0,
		{
			query: {
				enabled: !!service.data?.organisationId,
			},
		}
	);

	return (
		<List>
			<ListItemLink
				to={homeRoute({}).organisation({
					name: organisation.data?.routeName ?? '',
				})}
				text={organisation?.data?.name ?? ''}
				icon={<ArrowBackIcon />}
				onClick={handleDrawerToggle}
			/>
			<ListSubheader>{service?.data?.name}</ListSubheader>
			<ListItemLink
				to={homeRoute({})
					.service({ name: serviceRouteName })
					.releases({})}
				text={t('services.drawer.releases')}
				icon={<CorporateFare />}
				onClick={handleDrawerToggle}
			/>
		</List>
	);
};

export const useDrawer = () => {
	const setDrawerOpen = useSetRecoilState(drawerOpenState);

	const handleDrawerToggle = () => {
		setDrawerOpen((v) => !v);
	};

	const organisationRouteMatch = useMatch({
		path: organisationRoute.templateWithQuery,
		end: false,
	});
	const isOrganisationRoute = organisationRouteMatch !== null;
	const organisationRouteName = organisationRouteMatch?.params['name'] ?? '';

	const serviceRouteMatch = useMatch({
		// This is horrible.
		path: serviceRoute.templateWithQuery,
		end: false,
	});
	const isServiceRoute = serviceRouteMatch !== null;

	let drawer;

	if (isOrganisationRoute) {
		drawer = (
			<OrganisationDrawer
				name={organisationRouteName}
				handleDrawerToggle={handleDrawerToggle}
			/>
		);
	}

	if (isServiceRoute) {
		drawer = (
			<ServiceDrawer
				serviceRouteName={serviceRouteMatch.params['name']!}
				handleDrawerToggle={handleDrawerToggle}
			/>
		);
	}

	return drawer;
};

const Drawer: React.FC = () => {
	const [drawerOpen, setDrawerOpen] = useRecoilState(drawerOpenState);

	const handleDrawerToggle = () => {
		setDrawerOpen((v) => !v);
	};

	const drawer = useDrawer();

	return (
		<>
			{drawer && (
				<Box
					component='nav'
					sx={{
						display: 'flex',
						width: { sm: drawerWidth },
						flexShrink: { sm: 0 },
					}}
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
			)}
		</>
	);
};

export default Drawer;
