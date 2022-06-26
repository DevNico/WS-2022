import { CorporateFare } from '@mui/icons-material';
import ArrowBackIcon from '@mui/icons-material/ArrowBackIosNew';
import List from '@mui/material/List';
import ListSubheader from '@mui/material/ListSubheader';
import React from 'react';
import { useTranslation } from 'react-i18next';
import ListItemLink from '../../components/ListItemLink';
import { homeRoute } from '../../Router';
import BaseDrawer from './BaseDrawer';

const AdminDrawer: React.FC = () => {
	const { t } = useTranslation();

	return (
		<BaseDrawer>
			{(handleDrawerToggle) => (
				<List>
					<ListItemLink
						to={homeRoute({})}
						text={t('admin.drawer.home')}
						icon={<ArrowBackIcon />}
						onClick={handleDrawerToggle}
						disableMatch
					/>
					<ListSubheader>{t('admin.drawer.title')}</ListSubheader>
					<ListItemLink
						to={homeRoute({}).admin({}).organisations({})}
						text={t('admin.drawer.organisations')}
						icon={<CorporateFare />}
						onClick={handleDrawerToggle}
					/>
				</List>
			)}
		</BaseDrawer>
	);
};

export default AdminDrawer;
