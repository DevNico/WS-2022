import AddIcon from '@mui/icons-material/Add';
import Button from '@mui/material/Button/Button';
import Stack from '@mui/material/Stack/Stack';
import Typography from '@mui/material/Typography/Typography';
import React from 'react';
import toast from 'react-hot-toast';
import { useTranslation } from 'react-i18next';
import { useRouteParams } from 'typesafe-routes';
import { useOrganisationRolesList } from '../../../api/organisation-role/organisation-role';
import { useOrganisationsGetByRouteName } from '../../../api/organisation/organisation';
import CreateOrganisationRoleDialog from '../../components/organisationRole/CreateOrganisationRoleDialog';
import OrganisationRolesTable from '../../components/organisationRole/OrganisationRolesTable';
import { organisationRoute } from '../../Router';

const OrganisationRolesPage: React.FC = () => {
	const { name } = useRouteParams(organisationRoute);
	const organisation = useOrganisationsGetByRouteName(name);

	const { data, isLoading, isError, error } = useOrganisationRolesList(name);
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
					{t('organisation.roles.list.title')}
				</Typography>
				<Button
					onClick={handleOpenCreateDialog}
					startIcon={<AddIcon />}
					sx={{ width: 'max-content' }}
				>
					{t('organisation.roles.list.create')}
				</Button>
			</Stack>
			<OrganisationRolesTable
				roles={data ?? []}
				isLoading={isLoading}
				isError={isError}
			/>
			<CreateOrganisationRoleDialog
				organisation={organisation.data!}
				open={createDialogOpen}
				onClose={handleCloseCreateDialog}
			/>
		</>
	);
};

export default OrganisationRolesPage;
