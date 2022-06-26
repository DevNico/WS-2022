import { LinearProgress } from '@mui/material';
import { DataGrid, GridColDef } from '@mui/x-data-grid';
import React from 'react';
import { useTranslation } from 'react-i18next';
import { ServiceTemplateRecord } from '../../../api/models';
import DataGridOverlay from '../DataGridOverlay';

export interface ServiceTemplatesTableProps {
	templates: ServiceTemplateRecord[];
	isLoading: boolean;
	isError: boolean;
}

const ServiceTemplatesTable: React.FC<ServiceTemplatesTableProps> = ({
	templates,
	isLoading,
	isError,
}) => {
	const { t } = useTranslation();

	const columns: GridColDef<ServiceTemplateRecord>[] = [
		{
			field: 'email',
			headerName: t('serviceTemplate.model.name'),
			hideable: false,
			flex: 1,
		},
	];

	return (
		<DataGrid
			sx={{ minHeight: '500px' }}
			getRowId={(row) => row.id!}
			columns={columns}
			rows={templates}
			components={{
				LoadingOverlay: LinearProgress,
				NoRowsOverlay: () => (
					<DataGridOverlay
						text={
							isError
								? t('serviceTemplate.list.error')
								: t('serviceTemplate.list.noData')
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

export default ServiceTemplatesTable;
