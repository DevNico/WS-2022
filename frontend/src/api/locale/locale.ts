/**
 * Generated by orval v6.8.1 🍺
 * Do not edit manually.
 * Service Release Manager API
 * OpenAPI spec version: v1
 */
import {
	useQuery,
	useMutation,
	UseQueryOptions,
	UseMutationOptions,
	QueryFunction,
	MutationFunction,
	UseQueryResult,
	QueryKey,
} from 'react-query';
import type { LocaleRecord, CreateLocaleRequest } from '.././models';
import { customInstance, ErrorType } from '.././axios';

// eslint-disable-next-line
type SecondParameter<T extends (...args: any) => any> = T extends (
	config: any,
	args: infer P
) => any
	? P
	: never;

/**
 * Create a new locale
 * @summary Create a new locale
 */
export const localeCreate = (
	createLocaleRequest: CreateLocaleRequest,
	options?: SecondParameter<typeof customInstance>
) => {
	return customInstance<LocaleRecord>(
		{
			url: `/api/v1/locales`,
			method: 'post',
			headers: { 'Content-Type': 'application/json' },
			data: createLocaleRequest,
		},
		options
	);
};

export type LocaleCreateMutationResult = NonNullable<
	Awaited<ReturnType<typeof localeCreate>>
>;
export type LocaleCreateMutationBody = CreateLocaleRequest;
export type LocaleCreateMutationError = ErrorType<void>;

export const useLocaleCreate = <
	TError = ErrorType<void>,
	TContext = unknown
>(options?: {
	mutation?: UseMutationOptions<
		Awaited<ReturnType<typeof localeCreate>>,
		TError,
		{ data: CreateLocaleRequest },
		TContext
	>;
	request?: SecondParameter<typeof customInstance>;
}) => {
	const { mutation: mutationOptions, request: requestOptions } =
		options ?? {};

	const mutationFn: MutationFunction<
		Awaited<ReturnType<typeof localeCreate>>,
		{ data: CreateLocaleRequest }
	> = (props) => {
		const { data } = props ?? {};

		return localeCreate(data, requestOptions);
	};

	return useMutation<
		Awaited<ReturnType<typeof localeCreate>>,
		TError,
		{ data: CreateLocaleRequest },
		TContext
	>(mutationFn, mutationOptions);
};
/**
 * Deletes a locale from the database
 * @summary Delete a locale
 */
export const localeDelete = (
	localeId: number,
	options?: SecondParameter<typeof customInstance>
) => {
	return customInstance<void>(
		{ url: `/api/v1/locales/${localeId}`, method: 'delete' },
		options
	);
};

export type LocaleDeleteMutationResult = NonNullable<
	Awaited<ReturnType<typeof localeDelete>>
>;

export type LocaleDeleteMutationError = ErrorType<unknown>;

export const useLocaleDelete = <
	TError = ErrorType<unknown>,
	TContext = unknown
>(options?: {
	mutation?: UseMutationOptions<
		Awaited<ReturnType<typeof localeDelete>>,
		TError,
		{ localeId: number },
		TContext
	>;
	request?: SecondParameter<typeof customInstance>;
}) => {
	const { mutation: mutationOptions, request: requestOptions } =
		options ?? {};

	const mutationFn: MutationFunction<
		Awaited<ReturnType<typeof localeDelete>>,
		{ localeId: number }
	> = (props) => {
		const { localeId } = props ?? {};

		return localeDelete(localeId, requestOptions);
	};

	return useMutation<
		Awaited<ReturnType<typeof localeDelete>>,
		TError,
		{ localeId: number },
		TContext
	>(mutationFn, mutationOptions);
};
/**
 * Get a locale by id
 * @summary Get a locale by id
 */
export const localeGetById = (
	localeId: number,
	options?: SecondParameter<typeof customInstance>,
	signal?: AbortSignal
) => {
	return customInstance<LocaleRecord>(
		{ url: `/api/v1/locales/${localeId}`, method: 'get', signal },
		options
	);
};

export const getLocaleGetByIdQueryKey = (localeId: number) => [
	`/api/v1/locales/${localeId}`,
];

export type LocaleGetByIdQueryResult = NonNullable<
	Awaited<ReturnType<typeof localeGetById>>
>;
export type LocaleGetByIdQueryError = ErrorType<void>;

export const useLocaleGetById = <
	TData = Awaited<ReturnType<typeof localeGetById>>,
	TError = ErrorType<void>
>(
	localeId: number,
	options?: {
		query?: UseQueryOptions<
			Awaited<ReturnType<typeof localeGetById>>,
			TError,
			TData
		>;
		request?: SecondParameter<typeof customInstance>;
	}
): UseQueryResult<TData, TError> & { queryKey: QueryKey } => {
	const { query: queryOptions, request: requestOptions } = options ?? {};

	const queryKey =
		queryOptions?.queryKey ?? getLocaleGetByIdQueryKey(localeId);

	const queryFn: QueryFunction<Awaited<ReturnType<typeof localeGetById>>> = ({
		signal,
	}) => localeGetById(localeId, requestOptions, signal);

	const query = useQuery<
		Awaited<ReturnType<typeof localeGetById>>,
		TError,
		TData
	>(queryKey, queryFn, { enabled: !!localeId, ...queryOptions });

	return {
		queryKey,
		...query,
	};
};

/**
 * @summary List all locales
 */
export const localesList = (
	serviceId: number,
	options?: SecondParameter<typeof customInstance>,
	signal?: AbortSignal
) => {
	return customInstance<LocaleRecord[]>(
		{ url: `/api/v1/services/${serviceId}/locales`, method: 'get', signal },
		options
	);
};

export const getLocalesListQueryKey = (serviceId: number) => [
	`/api/v1/services/${serviceId}/locales`,
];

export type LocalesListQueryResult = NonNullable<
	Awaited<ReturnType<typeof localesList>>
>;
export type LocalesListQueryError = ErrorType<void>;

export const useLocalesList = <
	TData = Awaited<ReturnType<typeof localesList>>,
	TError = ErrorType<void>
>(
	serviceId: number,
	options?: {
		query?: UseQueryOptions<
			Awaited<ReturnType<typeof localesList>>,
			TError,
			TData
		>;
		request?: SecondParameter<typeof customInstance>;
	}
): UseQueryResult<TData, TError> & { queryKey: QueryKey } => {
	const { query: queryOptions, request: requestOptions } = options ?? {};

	const queryKey =
		queryOptions?.queryKey ?? getLocalesListQueryKey(serviceId);

	const queryFn: QueryFunction<Awaited<ReturnType<typeof localesList>>> = ({
		signal,
	}) => localesList(serviceId, requestOptions, signal);

	const query = useQuery<
		Awaited<ReturnType<typeof localesList>>,
		TError,
		TData
	>(queryKey, queryFn, { enabled: !!serviceId, ...queryOptions });

	return {
		queryKey,
		...query,
	};
};