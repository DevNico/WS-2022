import Grid from '@mui/material/Grid/Grid';
import React from 'react';
import { useTranslation } from 'react-i18next';
import { OrganisationRecord } from '../../api/models';
import { useOrganisationsMe } from '../../api/organisation/organisation';
import FullScreenError from '../components/layout/FullScreenError';
import OrganisationCard from '../components/organisation/OrganisationCard';
import QueryWrapper from '../components/QueryWrapper';

const ChooseOrganisationPage: React.FC = () => {
	const { t } = useTranslation();
	const organisationsResult = useOrganisationsMe();

	return (
		<QueryWrapper<OrganisationRecord[]> result={organisationsResult}>
			{(data) =>
				data!.length === 0 ? (
					<FullScreenError error={t('home.noOrganisations')} />
				) : (
					<Grid container spacing={[2, 4]}>
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
