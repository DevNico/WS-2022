import { LinearProgress } from '@mui/material';
import { DataGrid, GridColDef } from '@mui/x-data-grid';
import React from 'react';
import { useTranslation } from 'react-i18next';
import { ServiceTemplateRecord } from '../../../api/models';
import DataGridOverlay from '../DataGridOverlay';
import IconButton from '@mui/material/IconButton';
import DeleteIcon from '@mui/icons-material/Delete';
import toast from 'react-hot-toast';
import { getLocalesListQueryKey } from '../../../api/service/service';
import { useConfirm } from 'material-ui-confirm';
import { useMutation, useQueryClient } from 'react-query';
import {
	getServiceTemplateListQueryKey,
	serviceTemplateDelete,
} from '../../../api/service-template-endpoints/service-template-endpoints';

export interface ServiceTemplatesTableProps {
	organisationRouteName: string;
	templates: ServiceTemplateRecord[];
	isLoading: boolean;
	isError: boolean;
}

const ServiceTemplatesTable: React.FC<ServiceTemplatesTableProps> = ({
	organisationRouteName,
	templates,
	isLoading,
	isError,
}) => {
	const { t } = useTranslation();
	const confirm = useConfirm();
	const queryClient = useQueryClient();
	const deleteServiceTemplate = useMutation(serviceTemplateDelete);

	const handleDelete = async (templateId: number) => {
		const template = templates.find((t) => t.id === templateId);
		if (!template) return;

		await confirm({
			title: t('serviceTemplate.delete.confirm', { name: template.name }),
			confirmationText: t('common.confirmation.delete'),
			confirmationButtonProps: { color: 'error' },
		});

		await toast.promise(deleteServiceTemplate.mutateAsync(template.id!), {
			loading: t('common.loading'),
			success: t('serviceTemplate.delete.success'),
			error: t('serviceTemplate.delete.error'),
		});

		await queryClient.invalidateQueries(
			getServiceTemplateListQueryKey(organisationRouteName)
		);
	};

	const columns: GridColDef<ServiceTemplateRecord>[] = [
		{
			field: 'name',
			headerName: t('serviceTemplate.model.name'),
			hideable: false,
			flex: 1,
		},
		{
			field: 'id',
			headerName: t('serviceTemplate.model.actions'),
			renderCell: (rowData) => (
				<>
					<IconButton onClick={() => handleDelete(rowData.value)}>
						<DeleteIcon />
					</IconButton>
				</>
			),
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
