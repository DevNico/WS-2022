import React, { useEffect, useRef } from 'react';
import { DataGrid, GridColDef } from '@mui/x-data-grid';
import { Button, Grid, LinearProgress } from '@mui/material';
import { checkUserIsSuperAdminEffect } from '../../util';
import { useTranslation } from 'react-i18next';
import { useKeycloak } from '@react-keycloak/web';
import { useNavigate } from 'react-router-dom';
import { useMutation } from 'react-query';
import { organisationUserList } from '../../api/organisation-user/organisation-user';
import { organisationsList } from '../../api/organisation/organisation';
import AlertContainer from '../components/AlertContainer';
import CustomAlert from '../components/CustomAlert';
import EmptyTableOverlay from '../components/EmptyTableOverlay';

interface ColumnData {
	organisationName?: string;
	email?: string;
	firstName?: string;
	lastName?: string;
	lastSignIn?: string;
}

const UsersPage: React.FC = () => {
	const [loading, setLoading] = React.useState(true);
	const [data, setData] = React.useState<ColumnData[]>([]);
	const errorAlert = useRef<CustomAlert>(null);

	const { t } = useTranslation();
	const { keycloak } = useKeycloak();
	const navigate = useNavigate();
	const organisationsMutation = useMutation(organisationsList);
	const organisationUsersMutation = useMutation(organisationUserList);

	const columns: GridColDef<ColumnData>[] = [
		{
			field: 'organisationName',
			headerName: t('users.list.organisation'),
			hideable: false,
			flex: 1,
		},
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
						organisationUsersMutation
							.mutateAsync(o.routeName!)
							.then((users) =>
								users.map((u) => ({
									organisationName: o.name!,
									email: u.email!,
									firstName: u.firstName!,
									lastName: u.lastName!,
									lastSignIn: u.lastSignIn!,
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
			<Button
				variant='text'
				onClick={() => navigate('/users/create')}
				sx={{ width: 'max-content' }}
			>
				{t('users.list.create')}
			</Button>
			<DataGrid
				columns={columns}
				rows={data}
				components={{
					LoadingOverlay: LinearProgress,
					NoRowsOverlay: () => (
						<EmptyTableOverlay
							text={t('users.list.noData')}
							buttonText={t('users.list.create')}
							target='/users/create'
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
