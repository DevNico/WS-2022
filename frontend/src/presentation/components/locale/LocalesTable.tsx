import LinearProgress from '@mui/material/LinearProgress';
import { DataGrid, GridColDef } from '@mui/x-data-grid';
import React from 'react';
import { useTranslation } from 'react-i18next';
import { LocaleRecord, OrganisationUserRecord } from '../../../api/models';
import DataGridOverlay from '../DataGridOverlay';
import localeCodes from 'locale-codes';
import { renderBoolAsSymbol } from '../../../common/dataGridUtils';
import { useMutation, useQueryClient } from 'react-query';
import { localeDelete } from '../../../api/locale/locale';
import toast from 'react-hot-toast';
import { getLocalesListQueryKey } from '../../../api/service/service';
import IconButton from '@mui/material/IconButton';
import DeleteIcon from '@mui/icons-material/Delete';
import { useConfirm } from 'material-ui-confirm';

export interface LocalesTableProps {
	serviceRouteName: string;
	locales: LocaleRecord[];
	isLoading: boolean;
	isError: boolean;
}

const LocalesTable: React.FC<LocalesTableProps> = ({
	serviceRouteName,
	locales,
	isLoading,
	isError,
}) => {
	const { t } = useTranslation();

	const confirm = useConfirm();
	const queryClient = useQueryClient();
	const deleteLocale = useMutation(localeDelete);

	const handleDelete = async (localeId: number) => {
		const locale = locales.find((l) => l.id === localeId);
		if (!locale) return;

		await confirm({
			title: t('locale.delete.confirm', { tag: locale.tag }),
			confirmationText: t('common.confirmation.delete'),
			confirmationButtonProps: { color: 'error' },
		});

		await toast.promise(deleteLocale.mutateAsync(locale.id!), {
			loading: t('common.loading'),
			success: t('locale.delete.success'),
			error: t('locale.delete.error'),
		});

		queryClient.invalidateQueries(getLocalesListQueryKey(serviceRouteName));
	};

	const columns: GridColDef<OrganisationUserRecord>[] = [
		{
			field: 'tag',
			headerName: t('locale.model.tag'),
			hideable: false,
			maxWidth: 100,
		},
		{
			field: 'tag2',
			headerName: t('locale.model.language'),
			hideable: false,
			flex: 1,
			valueGetter: (params) => {
				const locale = localeCodes.getByTag(params.value);

				return `${locale.name} ${
					locale.location && `(${locale.location})`
				}`;
			},
		},
		{
			field: 'isDefault',
			headerName: t('locale.model.isDefault'),
			hideable: false,
			flex: 1,
			renderCell: renderBoolAsSymbol,
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
			rows={locales.map((locale) => ({ ...locale, tag2: locale.tag }))}
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

export default LocalesTable;
