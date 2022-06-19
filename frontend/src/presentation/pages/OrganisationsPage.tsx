import { DataGrid, GridColDef } from '@mui/x-data-grid';
import React from 'react';
import { OrganisationRecord } from '../../api/models';
import { useOrganisationsList } from '../../api/organisation/organisation';
import { dateValueFormatter, isSuperAdminState } from '../../util';
import { Button, Grid, LinearProgress } from '@mui/material';
import { useNavigate } from 'react-router-dom';
import EmptyTableOverlay from '../components/EmptyTableOverlay';
import { useTranslation } from 'react-i18next';
import { toast } from 'react-hot-toast';
import { useRecoilValue } from 'recoil';

const OrganisationsPage: React.FC = () => {
	const { data, isLoading, isError, error } = useOrganisationsList();
	const { t } = useTranslation();
	const navigate = useNavigate();

	const isSuperAdmin = useRecoilValue(isSuperAdminState);
	if (!isSuperAdmin) {
		navigate('/notFound');
		return <></>;
	}

	if (isError) {
		toast.error(error?.message);
	}

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
	];

	const createRoute = '/organisation/create';

	return (
		<Grid container rowGap={2} direction='column' height='100%'>
			<Button
				variant='text'
				onClick={() => navigate(createRoute)}
				sx={{ width: 'max-content' }}
			>
				{t('organisations.list.create')}
			</Button>
			<DataGrid
				columns={columns}
				rows={data ?? []}
				components={{
					LoadingOverlay: LinearProgress,
					NoRowsOverlay: () => (
						<EmptyTableOverlay
							text={t('organisations.list.noData')}
							buttonText={t('organisations.list.create')}
							target={createRoute}
						/>
					),
				}}
				loading={isLoading}
				disableColumnMenu
			/>
		</Grid>
	);
};

export default OrganisationsPage;
