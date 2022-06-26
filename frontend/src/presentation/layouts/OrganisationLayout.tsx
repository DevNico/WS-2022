import React from 'react';
import { Navigate, Outlet } from 'react-router-dom';
import { useRouteParams } from 'typesafe-routes';
import { useOrganisationsGetByRouteName } from '../../api/organisation/organisation';
import QueryWrapper from '../components/QueryWrapper';
import { organisationRoute } from '../Router';

const OrganisationLayout: React.FC = () => {
	const { name } = useRouteParams(organisationRoute);
	const organisation = useOrganisationsGetByRouteName(name);

	return (
		<QueryWrapper result={organisation} error={<Navigate to={'/404'} />}>
			{() => <Outlet />}
		</QueryWrapper>
	);
};

export default OrganisationLayout;
