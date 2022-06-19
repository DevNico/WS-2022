import React, { useEffect, useRef } from 'react';
import {DataGrid, GridColDef} from '@mui/x-data-grid';
import {Button, LinearProgress, Grid} from '@mui/material';
import { checkUserIsSuperAdminEffect } from '../../util';
import { useTranslation } from 'react-i18next';
import { useKeycloak } from '@react-keycloak/web';
import { useNavigate } from 'react-router-dom';
import { useMutation } from 'react-query';
import { organisationsList } from '../../api/organisation/organisation';
import AlertContainer from '../components/AlertContainer';
import CustomAlert from '../components/CustomAlert';
import { organisationRolesList } from '../../api/organisation-role/organisation-role';
import EmptyTableOverlay from '../components/EmptyTableOverlay';

interface ColumnData {
	id: number;
	organisationName?: string;
	name?: string;
	serviceWrite?: boolean;
	serviceDelete?: boolean;
	userRead?: boolean;
	userWrite?: boolean;
	userDelete?: boolean;
}

const UsersPage: React.FC = () => {
	const [loading, setLoading] = React.useState(true);
	const [data, setData] = React.useState<ColumnData[]>([]);
	const errorAlert = useRef<CustomAlert>(null);

	const { t } = useTranslation();
	const { keycloak } = useKeycloak();
	const navigate = useNavigate();
	const organisationsMutation = useMutation(organisationsList);
	const organisationRolesMutation = useMutation(organisationRolesList);

	const columns: GridColDef<ColumnData>[] = [
		{
			field: 'organisationName',
			headerName: t('users.list.organisation'),
			hideable: false,
			flex: 1,
		},
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

	useEffect(
		checkUserIsSuperAdminEffect.bind(
			null,
			keycloak,
			navigate,
			setLoading,
			async () => {
				try {
					const organisations =
						await organisationsMutation.mutateAsync(undefined);
					const promises = organisations.map((o) =>
						organisationRolesMutation
							.mutateAsync(o.routeName!)
							.then((roles) =>
								roles.map((r) => ({
									id: r.id!,
									organisationName: o.name!,
									name: r.name!,
									serviceWrite: r.serviceWrite!,
									serviceDelete: r.serviceDelete!,
									userRead: r.userRead!,
									userWrite: r.userWrite!,
									userDelete: r.userDelete!,
								}))
							)
					);

					const dt = await Promise.all(promises).then((results) =>
						results.flat()
					);
					setData(dt);
				} catch (e) {
					errorAlert.current?.show();
					throw e;
				} finally {
					setLoading(false);
				}
			}
		),
		[]
	);

	return (
		<Grid container rowGap={2} direction='column' height='100%'>
			<Button variant='text' onClick={() => navigate('/roles/create')} sx={{width: 'max-content'}}>
				{t('roles.list.create')}
			</Button>
			<DataGrid
				columns={columns}
				rows={data}
				components={{
					LoadingOverlay: LinearProgress,
					NoRowsOverlay: () => (
						<EmptyTableOverlay
							text={t('roles.list.noData')}
							buttonText={t('roles.list.create')}
							target='/roles/create'
						/>
					),
				}}
				loading={loading}
				disableColumnMenu
			/>
			<AlertContainer>
				<CustomAlert
					closeTimeout={-1}
					ref={errorAlert}
					severity='error'
				>
					{t('users.list.error')}
				</CustomAlert>
			</AlertContainer>
		</Grid>
	);
};

export default UsersPage;
