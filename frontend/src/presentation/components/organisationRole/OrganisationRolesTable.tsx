import { LinearProgress } from '@mui/material';
import { DataGrid, GridColDef, GridRenderCellParams } from '@mui/x-data-grid';
import React from 'react';
import { useTranslation } from 'react-i18next';
import { OrganisationRoleRecord } from '../../../api/models';
import { renderBoolAsSymbol } from '../../../common/dataGridUtils';
import DataGridOverlay from '../DataGridOverlay';

export interface OrganisationRolesTableProps {
	roles: OrganisationRoleRecord[];
	isLoading: boolean;
	isError: boolean;
}

const OrganisationRolesTable: React.FC<OrganisationRolesTableProps> = ({
	roles,
	isLoading,
	isError,
}) => {
	const { t } = useTranslation();

	const columns: GridColDef<OrganisationRoleRecord>[] = [
		{
			field: 'name',
			headerName: t('organisationRole.create.name'),
			hideable: false,
			flex: 1,
		},
		{
			field: 'serviceWrite',
			headerName: t('organisationRole.model.serviceWrite'),
			hideable: false,
			flex: 1,
			renderCell: renderBoolAsSymbol,
		},
		{
			field: 'serviceDelete',
			headerName: t('organisationRole.model.serviceDelete'),
			hideable: false,
			flex: 1,
			renderCell: renderBoolAsSymbol,
		},
		{
			field: 'userRead',
			headerName: t('organisationRole.model.userRead'),
			hideable: false,
			flex: 1,
			renderCell: renderBoolAsSymbol,
		},
		{
			field: 'userWrite',
			headerName: t('organisationRole.model.userWrite'),
			hideable: false,
			flex: 1,
			renderCell: renderBoolAsSymbol,
		},
		{
			field: 'userDelete',
			headerName: t('organisationRole.model.userDelete'),
			hideable: false,
			flex: 1,
			renderCell: renderBoolAsSymbol,
		},
	];

	return (
		<DataGrid
			sx={{ minHeight: '500px' }}
			columns={columns}
			rows={roles}
			components={{
				LoadingOverlay: LinearProgress,
				NoRowsOverlay: () => (
					<DataGridOverlay
						text={
							isError
								? t('organisationRole.list.error')
								: t('organisationRole.list.noData')
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

export default OrganisationRolesTable;
