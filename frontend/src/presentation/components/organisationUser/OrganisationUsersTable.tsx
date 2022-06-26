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
			headerName: t('organisation.users.model.email'),
			hideable: false,
			flex: 1,
		},
		{
			field: 'firstName',
			headerName: t('organisation.users.model.firstName'),
			hideable: false,
			flex: 1,
		},
		{
			field: 'lastName',
			headerName: t('organisation.users.model.lastName'),
			hideable: false,
			flex: 1,
		},
		{
			field: 'organisationRole',
			headerName: t('organisation.users.model.role'),
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
								? t('organisation.users.list.error')
								: t('organisation.users.list.noData')
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
