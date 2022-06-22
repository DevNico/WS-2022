import React from 'react';
import { useRouteParams } from 'typesafe-routes';
import { useOrganisationsGetByName } from '../../api/organisation/organisation';
import QueryWrapper from '../components/QueryWrapper';
import { organisationRoute } from '../Router';

const OrganisationPage: React.FC = () => {
	const { name } = useRouteParams(organisationRoute);
	const organisationResult = useOrganisationsGetByName(name);

	return (
		<QueryWrapper result={organisationResult}>
			{(organisation) => (
				<div>
					<h1>{organisation.name}</h1>
				</div>
			)}
		</QueryWrapper>
	);
};

export default OrganisationPage;
