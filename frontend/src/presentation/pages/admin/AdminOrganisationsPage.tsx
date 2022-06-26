import AddIcon from '@mui/icons-material/Add';
import Button from '@mui/material/Button/Button';
import Stack from '@mui/material/Stack/Stack';
import Typography from '@mui/material/Typography/Typography';
import React from 'react';
import { toast } from 'react-hot-toast';
import { useTranslation } from 'react-i18next';
import { useOrganisationsList } from '../../../api/organisation/organisation';
import CreateOrganisationDialog from '../../components/organisation/CreateOrganisationDialog';
import OrganisationsTable from '../../components/organisation/OrganisationsTable';

const AdminOrganisationsPage: React.FC = () => {
	const { data, isLoading, isError, error } = useOrganisationsList();
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
					{t('admin.organisations.title')}
				</Typography>
				<Button
					onClick={handleOpenCreateDialog}
					startIcon={<AddIcon />}
					sx={{ width: 'max-content' }}
				>
					{t('organisations.list.create')}
				</Button>
			</Stack>
			<OrganisationsTable
				organisations={data ?? []}
				isLoading={isLoading}
			/>
			<CreateOrganisationDialog
				open={createDialogOpen}
				onClose={handleCloseCreateDialog}
			/>
		</>
	);
};

export default AdminOrganisationsPage;
