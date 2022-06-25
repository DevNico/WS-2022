import Grid from '@mui/material/Grid/Grid';
import React, { useEffect, useState } from 'react';
import { useTranslation } from 'react-i18next';
import { useNavigate } from 'react-router-dom';
import { OrganisationRecord } from '../../api/models';
import { useOrganisationsMe } from '../../api/organisation/organisation';
import FullScreenError from '../components/layout/FullScreenError';
import OrganisationCard from '../components/organisation/OrganisationCard';
import QueryWrapper from '../components/QueryWrapper';
import { homeRoute } from '../Router';

const ChooseOrganisationPage: React.FC = () => {
	const { t } = useTranslation();
	const organisationsResult = useOrganisationsMe();
	const navigate = useNavigate();
	const [loading, setLoading] = useState(true);

	useEffect(() => {
		if (organisationsResult.data !== undefined) {
			if (organisationsResult.data?.length === 1) {
				navigate(
					homeRoute({}).organisation({
						name: organisationsResult.data[0].routeName!,
					}).$
				);
			} else {
				setLoading(false);
			}
		}
	}, organisationsResult.data);

	return (
		<QueryWrapper<OrganisationRecord[]>
			result={organisationsResult}
			overrideLoading={loading}
		>
			{(data) =>
				data!.length === 0 ? (
					<FullScreenError error={t('home.noOrganisations')} />
				) : (
					<Grid container spacing={[4, 2]}>
						{data!.map((organisation) => (
							<Grid
								item
								key={organisation.id}
								lg={3}
								md={6}
								sm={12}
								xs={12}
							>
								<OrganisationCard organisation={organisation} />
							</Grid>
						))}
					</Grid>
				)
			}
		</QueryWrapper>
	);
};

export default ChooseOrganisationPage;
