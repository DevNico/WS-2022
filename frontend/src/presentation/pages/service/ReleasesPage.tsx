import Stack from '@mui/material/Stack';
import Typography from '@mui/material/Typography';
import React from 'react';
import { useTranslation } from 'react-i18next';
import AddIcon from '@mui/icons-material/Add';
import { homeRoute, serviceRoute } from '../../Router';
import { useRouteParams } from 'typesafe-routes';
import {
	useServiceGetByRouteName,
	useServicesListReleases,
} from '../../../api/service/service';
import ReleasesTable from '../../components/release/ReleasesTable';
import toast from 'react-hot-toast';
import RouterButton from '../../components/RouterButton';

const ReleasesPage: React.FC = () => {
	const { t } = useTranslation();

	const { name } = useRouteParams(serviceRoute);
	const service = useServiceGetByRouteName(name);
	const serviceId = service.data?.id;

	const releases = useServicesListReleases(serviceId!, {
		query: {
			enabled: !!serviceId,
		},
	});

	const isLoading = releases.isLoading || service.isLoading;
	if (releases.isError) {
		toast.error(releases.error?.message);
	} else if (service.isError) {
		toast.error(service.error?.message);
	}

	return (
		<>
			<Stack
				direction={['column', 'column', 'row']}
				justifyContent='space-between'
				mb={2}
			>
				<Typography variant='h4' component='h2'>
					{t('release.list.title')}
				</Typography>
				<RouterButton
					to={homeRoute({}).service({ name }).releases({}).create({})}
					startIcon={<AddIcon />}
					sx={{ width: 'max-content' }}
				>
					{t('release.list.create')}
				</RouterButton>
			</Stack>
			<ReleasesTable
				serviceId={serviceId!}
				releases={releases.data ?? []}
				isLoading={isLoading}
				isError={releases.isError || service.isError}
			/>
		</>
	);
};

export default ReleasesPage;
