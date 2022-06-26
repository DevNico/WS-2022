import ArrowBackIcon from '@mui/icons-material/ArrowBackIosNew';
import Stack from '@mui/material/Stack';
import Typography from '@mui/material/Typography';
import React from 'react';
import { useTranslation } from 'react-i18next';
import { useRouteParams } from 'typesafe-routes';
import { OrganisationRecord } from '../../../api/models';
import { useOrganisationsGetByRouteName } from '../../../api/organisation/organisation';
import QueryWrapper from '../../components/QueryWrapper';
import RouterIconButton from '../../components/RouterIconButton';
import ServiceTemplateForm from '../../components/serviceTemplate/ServiceTemplateForm';
import { homeRoute, organisationRoute } from '../../Router';

const CreateServiceTemplatePage: React.FC = () => {
	const { t } = useTranslation();

	const { name } = useRouteParams(organisationRoute);
	const organisationResult = useOrganisationsGetByRouteName(name);

	return (
		<QueryWrapper<OrganisationRecord> result={organisationResult}>
			{(organisation) => (
				<Stack spacing={[2, 3]}>
					<Stack direction='row' spacing={[1, 2]}>
						<RouterIconButton
							to={homeRoute({})
								.organisation({ name })
								.serviceTemplates({})}
						>
							<ArrowBackIcon />
						</RouterIconButton>
						<Typography variant='h4' component='h2'>
							{t('serviceTemplate.create.title')}
						</Typography>
					</Stack>
					<ServiceTemplateForm
						onSubmitSuccess={() => {}}
						organisation={organisation}
					/>
				</Stack>
			)}
		</QueryWrapper>
	);
};

export default CreateServiceTemplatePage;
