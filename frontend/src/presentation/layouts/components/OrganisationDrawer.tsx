import AppsIcon from '@mui/icons-material/Apps';
import ArrowBackIcon from '@mui/icons-material/ArrowBackIosNew';
import ArticleIcon from '@mui/icons-material/Article';
import GroupIcon from '@mui/icons-material/Group';
import SecurityIcon from '@mui/icons-material/Security';
import List from '@mui/material/List/List';
import ListSubheader from '@mui/material/ListSubheader/ListSubheader';
import React from 'react';
import { useTranslation } from 'react-i18next';
import { useRouteParams } from 'typesafe-routes';
import { useOrganisationsGetByRouteName } from '../../../api/organisation/organisation';
import ListItemLink from '../../components/ListItemLink';
import { homeRoute, organisationRoute } from '../../Router';
import BaseDrawer from './BaseDrawer';

const OrganisationDrawer: React.FC = () => {
	const { t } = useTranslation();
	const { name } = useRouteParams(organisationRoute);
	const organisation = useOrganisationsGetByRouteName(name);

	return (
		<BaseDrawer>
			{(handleDrawerToggle) => (
				<List>
					<ListItemLink
						to={homeRoute({})}
						text={t('organisation.drawer.home')}
						icon={<ArrowBackIcon />}
						onClick={handleDrawerToggle}
						disableMatch
					/>
					<ListSubheader>{organisation?.data?.name}</ListSubheader>
					<ListItemLink
						to={homeRoute({}).organisation({ name }).services({})}
						text={t('organisation.drawer.services')}
						icon={<AppsIcon />}
						onClick={handleDrawerToggle}
					/>
					<ListItemLink
						to={homeRoute({})
							.organisation({ name })
							.serviceTemplates({})}
						text={t('organisation.drawer.serviceTemplates')}
						icon={<ArticleIcon />}
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
			)}
		</BaseDrawer>
	);
};

export default OrganisationDrawer;
