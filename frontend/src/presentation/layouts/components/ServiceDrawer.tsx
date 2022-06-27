import { CorporateFare } from '@mui/icons-material';
import ArrowBackIcon from '@mui/icons-material/ArrowBackIosNew';
import LanguageIcon from '@mui/icons-material/Language';
import List from '@mui/material/List/List';
import ListSubheader from '@mui/material/ListSubheader/ListSubheader';
import React from 'react';
import { useTranslation } from 'react-i18next';
import { useRouteParams } from 'typesafe-routes';
import { useOrganisationsGetById } from '../../../api/organisation/organisation';
import { useServiceGetByRouteName } from '../../../api/service/service';
import ListItemLink from '../../components/ListItemLink';
import { homeRoute, serviceRoute } from '../../Router';
import BaseDrawer from './BaseDrawer';

const ServiceDrawer: React.FC = () => {
	const { t } = useTranslation();
	const { name } = useRouteParams(serviceRoute);
	const service = useServiceGetByRouteName(name);
	const organisation = useOrganisationsGetById(
		service.data?.organisationId ?? 0,
		{
			query: {
				enabled: !!service.data?.organisationId,
			},
		}
	);

	return (
		<BaseDrawer>
			{(handleDrawerToggle) => (
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
						to={homeRoute({}).service({ name }).releases({})}
						text={t('service.drawer.releases')}
						icon={<CorporateFare />}
						onClick={handleDrawerToggle}
					/>
					<ListItemLink
						to={homeRoute({}).service({ name }).locales({})}
						text={t('service.drawer.locales')}
						icon={<LanguageIcon />}
						onClick={handleDrawerToggle}
					/>
				</List>
			)}
		</BaseDrawer>
	);
};

export default ServiceDrawer;
