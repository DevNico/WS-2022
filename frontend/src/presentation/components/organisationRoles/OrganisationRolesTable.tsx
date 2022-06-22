import { LinearProgress } from '@mui/material';
import { DataGrid, GridColDef, GridRenderCellParams } from '@mui/x-data-grid';
import React from 'react';
import { useTranslation } from 'react-i18next';
import { OrganisationRoleRecord } from '../../../api/models';
import { useOrganisationRolesList } from '../../../api/organisation-role/organisation-role';
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

	const renderBoolAsSymbol = (
		params: GridRenderCellParams<any, OrganisationRoleRecord, any>
	) => {
		return params.value ? '✔️' : '❌';
	};

	const columns: GridColDef<OrganisationRoleRecord>[] = [
		{
			field: 'name',
			headerName: t('organisation.roles.create.name'),
			hideable: false,
			flex: 1,
		},
		{
			field: 'serviceWrite',
			headerName: t('organisation.roles.create.serviceWrite'),
			hideable: false,
			flex: 1,
			renderCell: renderBoolAsSymbol,
		},
		{
			field: 'serviceDelete',
			headerName: t('organisation.roles.create.serviceDelete'),
			hideable: false,
			flex: 1,
			renderCell: renderBoolAsSymbol,
		},
		{
			field: 'userRead',
			headerName: t('organisation.roles.create.userRead'),
			hideable: false,
			flex: 1,
			renderCell: renderBoolAsSymbol,
		},
		{
			field: 'userWrite',
			headerName: t('organisation.roles.create.userWrite'),
			hideable: false,
			flex: 1,
			renderCell: renderBoolAsSymbol,
		},
		{
			field: 'userDelete',
			headerName: t('organisation.roles.create.userDelete'),
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
								? t('organisation.roles.list.error')
								: t('organisation.roles.list.noData')
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
