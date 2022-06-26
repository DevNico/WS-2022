import { LinearProgress } from '@mui/material';
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

export interface LocalesTableProps {
	locales: LocaleRecord[];
	isLoading: boolean;
	isError: boolean;
}

const LocalesTable: React.FC<LocalesTableProps> = ({
	locales,
	isLoading,
	isError,
}) => {
	const { t } = useTranslation();

	const queryClient = useQueryClient();
	const deleteLocale = useMutation(localeDelete);

	const handleDelete = async () => {
		// toast.promise();
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
					{/* <RouterIconButton
						to={homeRoute({}).organisation({ name: rowData.value })}
					>
						<VisibilityIcon />
					</RouterIconButton> */}
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
