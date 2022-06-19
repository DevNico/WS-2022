import { DataGrid, GridColDef } from '@mui/x-data-grid';
import React, { useEffect, useRef } from 'react';
import { OrganisationRecord } from '../../api/models';
import { useOrganisationsList } from '../../api/organisation/organisation';
import { dateValueFormatter } from '../../util';
import {Button, Grid, LinearProgress} from '@mui/material';
import AlertContainer from '../components/AlertContainer';
import CustomAlert from '../components/CustomAlert';
import { useKeycloak } from '@react-keycloak/web';
import { useNavigate } from 'react-router-dom';
import EmptyTableOverlay from "../components/EmptyTableOverlay";
import {useTranslation} from "react-i18next";

const OrganisationsPage: React.FC = () => {
	const { data, isLoading, isError, error } = useOrganisationsList();
	const errorAlert = useRef<CustomAlert>(null);
	const { keycloak } = useKeycloak();
	const { t } = useTranslation();
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
			headerName: t('organisations.list.name'),
			hideable: false,
			flex: 2,
			minWidth: 150,
		},
		{
			field: 'description',
			headerName: t('organisations.list.description'),
			flex: 4,
		},
		{
			field: 'createdAt',
			headerName: t('organisations.list.createdAt'),
			flex: 1,
			valueFormatter: dateValueFormatter,
			minWidth: 130,
		},
	];

	return (
		<Grid container rowGap={2} direction='column' height='100%'>
			<Button variant='text' onClick={() => navigate('/organisations/create')} sx={{width: 'max-content'}}>
				{t('organisations.list.create')}
			</Button>
			<DataGrid
				columns={columns}
				rows={data ?? []}
				components={{
					LoadingOverlay: LinearProgress,
					NoRowsOverlay: () => (
						<EmptyTableOverlay
							text={t('organisations.list.noData')}
							buttonText={t('organisations.list.create')}
							target='/organisations/create'
						/>
					),
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
		</Grid>
	);
};

export default OrganisationsPage;
