import React from 'react';
import { DataGrid, GridColDef } from '@mui/x-data-grid';
import { Button, Grid, LinearProgress } from '@mui/material';
import { isSuperAdminState } from '../../util';
import { useTranslation } from 'react-i18next';
import { useNavigate, useParams } from 'react-router-dom';
import { useOrganisationUserList } from '../../api/organisation-user/organisation-user';
import EmptyTableOverlay from '../components/EmptyTableOverlay';
import { toast } from 'react-hot-toast';
import { useRecoilValue } from 'recoil';

interface ColumnData {
	email?: string;
	firstName?: string;
	lastName?: string;
	lastSignIn?: string;
}

const UsersPage: React.FC = () => {
	const { t } = useTranslation();
	const { name } = useParams();
	const navigate = useNavigate();

	const users = useOrganisationUserList(name!);
	const isSuperAdmin = useRecoilValue(isSuperAdminState);

	if (!isSuperAdmin) {
		navigate('/notFound');
		return <></>;
	} else if (users.isError) {
		toast.error(t('users.list.error'));
		navigate('/notFound');
		return <></>;
	}

	const columns: GridColDef<ColumnData>[] = [
		{
			field: 'email',
			headerName: t('users.list.email'),
			hideable: false,
			flex: 1,
		},
		{
			field: 'firstName',
			headerName: t('users.list.firstName'),
			hideable: false,
			flex: 1,
		},
		{
			field: 'lastName',
			headerName: t('users.list.lastName'),
			hideable: false,
			flex: 1,
		},
		{
			field: 'lastSignIn',
			headerName: t('users.list.lastSignIn'),
			hideable: false,
			flex: 1,
		},
	];

	const createRoute = `/organisation/${name}/user/create`;

	return (
		<Grid container rowGap={2} direction='column' height='100%'>
			<Button
				variant='text'
				onClick={() => navigate(createRoute)}
				sx={{ width: 'max-content' }}
			>
				{t('users.list.create')}
			</Button>
			<DataGrid
				columns={columns}
				rows={
					users.data?.map((u) => ({
						email: u.email!,
						firstName: u.firstName!,
						lastName: u.lastName!,
						lastSignIn: u.lastSignIn!,
					})) ?? []
				}
				components={{
					LoadingOverlay: LinearProgress,
					NoRowsOverlay: () => (
						<EmptyTableOverlay
							text={t('users.list.noData')}
							buttonText={t('users.list.create')}
							target={createRoute}
						/>
					),
				}}
				loading={users.isLoading}
				disableColumnMenu
			/>
		</Grid>
	);
};

export default UsersPage;
