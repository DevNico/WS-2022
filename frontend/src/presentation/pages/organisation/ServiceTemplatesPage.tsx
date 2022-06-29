import AddIcon from '@mui/icons-material/Add';
import Stack from '@mui/material/Stack';
import Typography from '@mui/material/Typography';
import React from 'react';
import { useTranslation } from 'react-i18next';
import { useRouteParams } from 'typesafe-routes';
import RouterButton from '../../components/RouterButton';
import { homeRoute, organisationRoute } from '../../Router';
import ServiceTemplatesTable from '../../components/serviceTemplate/ServiceTemplatesTable';
import toast from 'react-hot-toast';
import { useOrganisationListServiceTemplates } from '../../../api/organisation/organisation';

const ServiceTemplatesPage: React.FC = () => {
	const { name } = useRouteParams(organisationRoute);
	const { t } = useTranslation();

	const { data, isLoading, isError, error } =
		useOrganisationListServiceTemplates(name);

	if (isError) {
		toast.error(error?.message);
	}

	return (
		<>
			<Stack
				direction={['column', 'column', 'row']}
				justifyContent='space-between'
				mb={2}
			>
				<Typography variant='h4' component='h2'>
					{t('serviceTemplate.list.title')}
				</Typography>
				<RouterButton
					to={homeRoute({})
						.organisation({ name })
						.serviceTemplates({})
						.create({})}
					startIcon={<AddIcon />}
					sx={{ width: 'max-content' }}
				>
					{t('serviceTemplate.list.create')}
				</RouterButton>
			</Stack>
			<ServiceTemplatesTable
				organisationRouteName={name}
				templates={data ?? []}
				isLoading={isLoading}
				isError={isError}
			/>
		</>
	);
};

export default ServiceTemplatesPage;
