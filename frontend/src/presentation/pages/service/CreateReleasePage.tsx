import React from 'react';
import QueryWrapper from '../../components/QueryWrapper';
import { ServiceRecord } from '../../../api/models';
import { useRouteParams } from 'typesafe-routes';
import { homeRoute, serviceRoute } from '../../Router';
import { useServiceGetByRouteName } from '../../../api/service/service';
import { Stack } from '@mui/material';
import RouterIconButton from '../../components/RouterIconButton';
import ArrowBackIcon from '@mui/icons-material/ArrowBackIosNew';
import Typography from '@mui/material/Typography';
import { useTranslation } from 'react-i18next';
import ReleaseForm from '../../components/release/ReleaseForm';

const CreateReleasePage: React.FC = () => {
	const { t } = useTranslation();
	const { name } = useRouteParams(serviceRoute);
	const service = useServiceGetByRouteName(name);

	return (
		<QueryWrapper<ServiceRecord> result={service}>
			{(service) => (
				<>
					<Stack spacing={[2, 3]}>
						<Stack direction='row' spacing={[1, 2]}>
							<RouterIconButton
								to={homeRoute({})
									.service({ name })
									.releases({})}
							>
								<ArrowBackIcon />
							</RouterIconButton>
							<Typography variant='h4' component='h2'>
								{t('release.create.title')}
							</Typography>
						</Stack>
						<ReleaseForm
							service={service}
							onSubmitSuccess={() => {}}
						/>
					</Stack>
				</>
			)}
		</QueryWrapper>
	);
};

export default CreateReleasePage;
