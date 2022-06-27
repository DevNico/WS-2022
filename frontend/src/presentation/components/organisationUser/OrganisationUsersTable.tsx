import { LinearProgress } from '@mui/material';
import { DataGrid, GridColDef } from '@mui/x-data-grid';
import React from 'react';
import { useTranslation } from 'react-i18next';
import { OrganisationUserRecord } from '../../../api/models';
import DataGridOverlay from '../DataGridOverlay';

export interface OrganisationUsersTableProps {
	users: OrganisationUserRecord[];
	isLoading: boolean;
	isError: boolean;
}

const OrganisationUsersTable: React.FC<OrganisationUsersTableProps> = ({
	users,
	isLoading,
	isError,
}) => {
	const { t } = useTranslation();

	const columns: GridColDef<OrganisationUserRecord>[] = [
		{
			field: 'email',
			headerName: t('organisationUser.model.email'),
			hideable: false,
			flex: 1,
		},
		{
			field: 'firstName',
			headerName: t('organisationUser.model.firstName'),
			hideable: false,
			flex: 1,
		},
		{
			field: 'lastName',
			headerName: t('organisationUser.model.lastName'),
			hideable: false,
			flex: 1,
		},
		{
			field: 'organisationRole',
			headerName: t('organisationUser.model.role'),
			hideable: false,
			flex: 1,
			valueGetter: (params) => {
				return `${params.value.name}`;
			},
		},
	];

	return (
		<DataGrid
			sx={{ minHeight: '500px' }}
			getRowId={(row) => row.id!}
			columns={columns}
			rows={users}
			components={{
				LoadingOverlay: LinearProgress,
				NoRowsOverlay: () => (
					<DataGridOverlay
						text={
							isError
								? t('organisationUser.list.error')
								: t('organisationUser.list.noData')
						}
					/>
				),
			}}
			loading={isLoading}
			disableColumnMenu
			autoHeight
		/>
	);
};

export default OrganisationUsersTable;
