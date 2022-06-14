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

	const columns: GridColDef<OrganisationRecord>[] = [
		{
			field: 'name',
			headerName: 'Name',
		},
		{
			field: 'description',
			headerName: 'Description',
		},
		{
			field: 'createdAt',
			headerName: 'Created At',
		},
		{
			field: 'updatedAt',
			headerName: 'Updated At',
		},
	];

	return <DataGrid columns={columns} rows={data ?? []} />;
};

export default OrganisationsPage;
