import { GridRenderCellParams } from '@mui/x-data-grid';

export const renderBoolAsSymbol = (params: GridRenderCellParams) => {
	return params.value ? '✔️' : '❌';
};
