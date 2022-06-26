import AddIcon from '@mui/icons-material/Add';
import Button from '@mui/material/Button/Button';
import Stack from '@mui/material/Stack/Stack';
import Typography from '@mui/material/Typography/Typography';
import React from 'react';
import toast from 'react-hot-toast';
import { useTranslation } from 'react-i18next';
import { useRouteParams } from 'typesafe-routes';
import { useOrganisationUserList } from '../../../api/organisation-user/organisation-user';
import { useOrganisationsGetByRouteName } from '../../../api/organisation/organisation';
import CreateOrganisationUserDialog from '../../components/organisationUser/CreateOrganisationUserDialog';
import OrganisationUsersTable from '../../components/organisationUser/OrganisationUsersTable';
import { organisationRoute } from '../../Router';

const OrganisationUsersPage: React.FC = () => {
	const { name } = useRouteParams(organisationRoute);
	const organisation = useOrganisationsGetByRouteName(name);

	const { data, isLoading, isError, error } = useOrganisationUserList(name);
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
					{t('organisationUser.list.title')}
				</Typography>
				<Button
					onClick={handleOpenCreateDialog}
					startIcon={<AddIcon />}
					sx={{ width: 'max-content' }}
				>
					{t('organisationUser.list.create')}
				</Button>
			</Stack>
			<OrganisationUsersTable
				users={data ?? []}
				isLoading={isLoading}
				isError={isError}
			/>
			<CreateOrganisationUserDialog
				organisation={organisation.data!}
				open={createDialogOpen}
				onClose={handleCloseCreateDialog}
			/>
		</>
	);
};

export default OrganisationUsersPage;
