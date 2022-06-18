import { DataGrid, GridColDef } from '@mui/x-data-grid';
import React from 'react';
import { OrganisationRecord } from '../../api/models';
import { useOrganisationsList } from '../../api/organisation/organisation';

const OrganisationsPage: React.FC = () => {
	const { data, isLoading, isError, error } = useOrganisationsList();

	if (isLoading) {
		return <span>Loading...</span>;
	}

	if (isError) {
		return <span>Error: {error.message}</span>;
	}

	const dateValueFormatter = (params: any) => {
		return new Date(params.value).toLocaleDateString();
	};

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

	return <DataGrid columns={columns} rows={data ?? []} disableColumnMenu />;
};

export default OrganisationsPage;
