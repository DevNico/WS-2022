import AddIcon from '@mui/icons-material/Add';
import Button from '@mui/material/Button/Button';
import Stack from '@mui/material/Stack/Stack';
import Typography from '@mui/material/Typography/Typography';
import React from 'react';
import toast from 'react-hot-toast';
import { useTranslation } from 'react-i18next';
import { useRouteParams } from 'typesafe-routes';
import {
	useLocalesList,
	useServiceGetByRouteName,
} from '../../../api/service/service';
import CreateLocaleDialog from '../../components/locale/CreateLocaleDialog';
import LocalesTable from '../../components/locale/LocalesTable';
import { serviceRoute } from '../../Router';

const LocalesPage: React.FC = () => {
	const { name } = useRouteParams(serviceRoute);

	const service = useServiceGetByRouteName(name);
	const { data, isLoading, isError, error } = useLocalesList(name);
	const { t } = useTranslation();

	if (isError) {
		toast.error(error?.message);
	}

	const [createDialogOpen, setCreateDialogOpen] = React.useState(false);

	const handleOpenCreateDialog = () => {
		setCreateDialogOpen(true);
	};

	const handleCloseCreateDialog = () => {
		setCreateDialogOpen(false);
	};

	return (
		<>
			<Stack
				direction={['column', 'column', 'row']}
				justifyContent='space-between'
				mb={2}
			>
				<Typography variant='h4' component='h2'>
					{t('locale.list.title')}
				</Typography>
				<Button
					onClick={handleOpenCreateDialog}
					startIcon={<AddIcon />}
					sx={{ width: 'max-content' }}
				>
					{t('locale.list.create')}
				</Button>
			</Stack>
			<LocalesTable
				serviceRouteName={name}
				locales={data ?? []}
				isLoading={isLoading}
				isError={isError}
			/>
			<CreateLocaleDialog
				service={service.data!}
				open={createDialogOpen}
				onClose={handleCloseCreateDialog}
			/>
		</>
	);
};

export default LocalesPage;
