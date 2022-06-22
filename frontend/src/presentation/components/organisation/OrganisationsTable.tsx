import LinearProgress from '@mui/material/LinearProgress/LinearProgress';
import { DataGrid, GridColDef } from '@mui/x-data-grid';
import React from 'react';
import { useTranslation } from 'react-i18next';
import { OrganisationRecord } from '../../../api/models';
import { dateValueFormatter } from '../../../util';
import { homeRoute } from '../../Router';
import DataGridOverlay from '../DataGridOverlay';
import RouterIconButton from '../RouterIconButton';
import VisibilityIcon from '@mui/icons-material/Visibility';

export interface OrganisationsTableProps {
	organisations: OrganisationRecord[];
	isLoading: boolean;
}

const OrganisationsTable: React.FC<OrganisationsTableProps> = ({
	organisations,
	isLoading,
}) => {
	const { t } = useTranslation();

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
		{
			field: 'routeName',
			headerName: t('organisations.list.actions'),
			renderCell: (rowData) => (
				<>
					<RouterIconButton
						to={homeRoute({}).organisation({ name: rowData.value })}
					>
						<VisibilityIcon />
					</RouterIconButton>
				</>
			),
		},
	];

	return (
		<DataGrid
			sx={{ minHeight: '500px' }}
			columns={columns}
			rows={organisations}
			components={{
				LoadingOverlay: LinearProgress,
				NoRowsOverlay: () => (
					<DataGridOverlay
						text={t('organisations.list.noData')}
						// buttonText={t('organisations.list.create')}
						// target={createRoute}
					/>
				),
			}}
			loading={isLoading}
			disableColumnMenu
			autoHeight
		/>
	);
};

export default OrganisationsTable;
