import { DataGrid, GridColDef } from '@mui/x-data-grid';
import React, { useEffect, useRef } from 'react';
import { OrganisationRecord } from '../../api/models';
import { useOrganisationsList } from '../../api/organisation/organisation';
import { dateValueFormatter } from '../../util';
import { LinearProgress } from '@mui/material';
import AlertContainer from '../components/AlertContainer';
import CustomAlert from '../components/CustomAlert';
import { useKeycloak } from '@react-keycloak/web';
import { useNavigate } from 'react-router-dom';

const OrganisationsPage: React.FC = () => {
	const { data, isLoading, isError, error } = useOrganisationsList();
	const errorAlert = useRef<CustomAlert>(null);
	const { keycloak } = useKeycloak();
	const navigate = useNavigate();

	useEffect(() => {
		if (!keycloak.authenticated) {
			navigate('/unauthorized');
			return;
		}

		keycloak.loadUserInfo().then(async () => {
			if (!keycloak.hasRealmRole('superAdmin')) {
				navigate('/unauthorized');
			}
		});
	});

	if (isError) {
		errorAlert.current?.show();
	}

	const columns: GridColDef<OrganisationRecord>[] = [
		{
			field: 'name',
			headerName: 'Name',
			hideable: false,
			flex: 2,
			minWidth: 150,
		},
		{
			field: 'description',
			headerName: 'Description',
			flex: 4,
		},
		{
			field: 'createdAt',
			headerName: 'Created At',
			flex: 1,
			valueFormatter: dateValueFormatter,
			minWidth: 130,
		},
	];

	return (
		<>
			<DataGrid
				columns={columns}
				rows={data ?? []}
				components={{
					LoadingOverlay: LinearProgress,
				}}
				loading={isLoading}
				disableColumnMenu
			/>
			<AlertContainer>
				<CustomAlert
					closeTimeout={-1}
					ref={errorAlert}
					severity='error'
				>
					{error?.message}
				</CustomAlert>
			</AlertContainer>
		</>
	);
};

export default OrganisationsPage;
