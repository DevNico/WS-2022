import React from 'react';
import { useTranslation } from 'react-i18next';
import { UseQueryResult } from 'react-query';
import { ErrorType } from '../../api/axios';
import FullScreenError from './layout/FullScreenError';
import FullScreenLoading from './layout/FullScreenLoading';

export interface QueryWrapperProps<TData, TError> {
	result: UseQueryResult<TData, TError>;
	overrideLoading?: boolean;
	loading?: React.ReactNode;
	error?: React.ReactNode;
	loadingError?: string;
	children: (data: TData) => React.ReactNode;
}

function QueryWrapper<TData>({
	result,
	overrideLoading,
	loading,
	error,
	loadingError,
	children,
}: QueryWrapperProps<TData, ErrorType<any>>) {
	const { isLoading, isError, data, refetch } = result;

	const { t } = useTranslation();

	if (isLoading || overrideLoading) {
		if (loading) {
			return <>{loading}</>;
		}

		return <FullScreenLoading />;
	}

	if (isError) {
		if (error) {
			return <>{error}</>;
		}

		return (
			<FullScreenError
				error={loadingError ?? t('common.error.loading')}
				retry={refetch}
			/>
		);
	}

	return <>{data && children(data)}</>;
}

export default QueryWrapper;
