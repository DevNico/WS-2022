import React from 'react';
import { UseQueryResult } from 'react-query';
import { TaggedTemplateExpression } from 'typescript';
import FullScreenLoading from './layout/FullScreenLoading';
import FullScreenError from './layout/FullScreenError';
import { ErrorType } from '../../api/axios';
import { useTranslation } from 'react-i18next';

export interface QueryWrapperProps<TData, TError> {
	result: UseQueryResult<TData, TError>;
	loading?: React.ReactNode;
	error?: React.ReactNode;
	loadingError?: string;
	children: (data: TData) => React.ReactNode;
}

function QueryWrapper<TData>({
	result,
	loading,
	error,
	loadingError,
	children,
}: QueryWrapperProps<TData, ErrorType<any>>) {
	const { isLoading, isError, data, refetch } = result;

	const { t } = useTranslation();

	if (isLoading) {
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
