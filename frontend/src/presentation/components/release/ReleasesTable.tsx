import LinearProgress from '@mui/material/LinearProgress';
import { DataGrid, GridColDef } from '@mui/x-data-grid';
import React from 'react';
import { useTranslation } from 'react-i18next';
import { ReleaseRecord } from '../../../api/models';
import DataGridOverlay from '../DataGridOverlay';
import { useMutation, useQueryClient } from 'react-query';
import toast from 'react-hot-toast';
import { getServicesListReleasesQueryKey } from '../../../api/service/service';
import IconButton from '@mui/material/IconButton';
import DeleteIcon from '@mui/icons-material/Delete';
import { useConfirm } from 'material-ui-confirm';
import { releaseDelete } from '../../../api/release/release';
import { dateValueFormatter } from '../../../util';

export interface LocalesTableProps {
	serviceId: number;
	releases: ReleaseRecord[];
	isLoading: boolean;
	isError: boolean;
}

const ReleasesTable: React.FC<LocalesTableProps> = ({
	serviceId,
	releases,
	isLoading,
	isError,
}) => {
	const { t } = useTranslation();

	const confirm = useConfirm();
	const queryClient = useQueryClient();
	const deleteRelease = useMutation(releaseDelete);

	const handleDelete = async (releaseId: number) => {
		const release = releases.find((r) => r.id === releaseId);
		if (!release) return;

		await confirm({
			title: t('release.delete.confirm', { name: release.version }),
			confirmationText: t('common.confirmation.delete'),
			confirmationButtonProps: { color: 'error' },
		});

		await toast.promise(deleteRelease.mutateAsync(release.id!), {
			loading: t('common.loading'),
			success: t('release.delete.success'),
			error: t('release.delete.error'),
		});

		await queryClient.invalidateQueries(
			getServicesListReleasesQueryKey(serviceId)
		);
	};

	const columns: GridColDef<ReleaseRecord>[] = [
		{
			field: 'version',
			headerName: t('release.model.version'),
			hideable: false,
			maxWidth: 100,
		},
		{
			field: 'createdAt',
			headerName: t('release.model.createdAt'),
			hideable: false,
			flex: 1,
			valueFormatter: dateValueFormatter,
		},
		{
			field: 'id',
			headerName: t('locale.list.actions'),
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
			rows={releases}
			components={{
				LoadingOverlay: LinearProgress,
				NoRowsOverlay: () => (
					<DataGridOverlay
						text={
							isError
								? t('locale.list.error')
								: t('locale.list.noData')
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

export default ReleasesTable;
