import React from 'react';
import { Navigate } from 'react-router-dom';
import { useRouteParams } from 'typesafe-routes';
import { useOrganisationsGetByRouteName } from '../../api/organisation/organisation';
import QueryWrapper from '../components/QueryWrapper';
import { organisationRoute } from '../Router';
import BaseLayout from './BaseLayout';
import OrganisationDrawer from './components/OrganisationDrawer';

const OrganisationLayout: React.FC = () => {
	const { name } = useRouteParams(organisationRoute);
	const organisation = useOrganisationsGetByRouteName(name);

	return (
		<QueryWrapper result={organisation} error={<Navigate to={'/404'} />}>
			{() => <BaseLayout drawer={<OrganisationDrawer />} />}
		</QueryWrapper>
	);
};

export default OrganisationLayout;
