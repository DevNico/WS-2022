import React from 'react';
import { DataGrid, GridColDef } from '@mui/x-data-grid';
import { Button, LinearProgress, Grid } from '@mui/material';
import { isSuperAdminState } from '../../util';
import { useTranslation } from 'react-i18next';
import { useNavigate, useParams } from 'react-router-dom';
import { useOrganisationRolesList } from '../../api/organisation-role/organisation-role';
import EmptyTableOverlay from '../components/EmptyTableOverlay';
import { toast } from 'react-hot-toast';
import { useRecoilValue } from 'recoil';

interface ColumnData {
	id: number;
	name?: string;
	serviceWrite?: boolean;
	serviceDelete?: boolean;
	userRead?: boolean;
	userWrite?: boolean;
	userDelete?: boolean;
}

const UsersPage: React.FC = () => {
	const { t } = useTranslation();
	const navigate = useNavigate();
	const { name } = useParams();
	const isSuperAdmin = useRecoilValue(isSuperAdminState);
	const roles = useOrganisationRolesList(name!);

	if (!isSuperAdmin) {
		navigate('/notFound');
		return <></>;
	} else if (roles.isError) {
		toast.error(t('roles.list.error'));
		navigate('/notFound');
		return <></>;
	}

	const columns: GridColDef<ColumnData>[] = [
		{
			field: 'name',
			headerName: t('roles.create.name'),
			hideable: false,
			flex: 1,
		},
		{
			field: 'serviceWrite',
			headerName: t('roles.create.serviceWrite'),
			hideable: false,
			flex: 1,
		},
		{
			field: 'serviceDelete',
			headerName: t('roles.create.serviceDelete'),
			hideable: false,
			flex: 1,
		},
		{
			field: 'userRead',
			headerName: t('roles.create.userRead'),
			hideable: false,
			flex: 1,
		},
		{
			field: 'userWrite',
			headerName: t('roles.create.userWrite'),
			hideable: false,
			flex: 1,
		},
		{
			field: 'userDelete',
			headerName: t('roles.create.userDelete'),
			hideable: false,
			flex: 1,
		},
	];

	const createRoute = `/organisation/${name}/role/create`;

	return (
		<Grid container rowGap={2} direction='column' height='100%'>
			<Button
				variant='text'
				onClick={() => navigate(createRoute)}
				sx={{ width: 'max-content' }}
			>
				{t('roles.list.create')}
			</Button>
			<DataGrid
				columns={columns}
				rows={
					roles.data?.map((r) => ({
						id: r.id!,
						name: r.name!,
						serviceWrite: r.serviceWrite!,
						serviceDelete: r.serviceDelete!,
						userRead: r.userRead!,
						userWrite: r.userWrite!,
						userDelete: r.userDelete!,
					})) ?? []
				}
				components={{
					LoadingOverlay: LinearProgress,
					NoRowsOverlay: () => (
						<EmptyTableOverlay
							text={t('roles.list.noData')}
							buttonText={t('roles.list.create')}
							target={createRoute}
						/>
					),
				}}
				loading={roles.isLoading}
				disableColumnMenu
			/>
		</Grid>
	);
};

export default UsersPage;
