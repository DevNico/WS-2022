/**
 * Generated by orval v6.8.1 🍺
 * Do not edit manually.
 * Service Release Manager API
 * OpenAPI spec version: v1
 */
import {
	MutationFunction,
	QueryFunction,
	QueryKey,
	useMutation,
	UseMutationOptions,
	useQuery,
	UseQueryOptions,
	UseQueryResult,
} from 'react-query';
import { customInstance, ErrorType } from '.././axios';
import type {
	CreateOrganisationRequest,
	ErrorResponse,
	OrganisationRecord,
	OrganisationsListParams,
	ServiceRecord,
	ServiceRoleRecord,
} from '.././models';

// eslint-disable-next-line
type SecondParameter<T extends (...args: any) => any> = T extends (
	config: any,
	args: infer P
) => any
	? P
	: never;

/**
 * Creates a new Organisation
 * @summary Creates a new Organisation
 */
export const organisationCreate = (
	createOrganisationRequest: CreateOrganisationRequest,
	options?: SecondParameter<typeof customInstance>
) => {
	return customInstance<OrganisationRecord>(
		{
			url: `/api/v1/organisations`,
			method: 'post',
			headers: { 'Content-Type': 'application/json' },
			data: createOrganisationRequest,
		},
		options
	);
};

export type OrganisationCreateMutationResult = NonNullable<
	Awaited<ReturnType<typeof organisationCreate>>
>;
export type OrganisationCreateMutationBody = CreateOrganisationRequest;
export type OrganisationCreateMutationError = ErrorType<ErrorResponse | void>;

export const useOrganisationCreate = <
	TError = ErrorType<ErrorResponse | void>,
	TContext = unknown
>(options?: {
	mutation?: UseMutationOptions<
		Awaited<ReturnType<typeof organisationCreate>>,
		TError,
		{ data: CreateOrganisationRequest },
		TContext
	>;
	request?: SecondParameter<typeof customInstance>;
}) => {
	const { mutation: mutationOptions, request: requestOptions } =
		options ?? {};

	const mutationFn: MutationFunction<
		Awaited<ReturnType<typeof organisationCreate>>,
		{ data: CreateOrganisationRequest }
	> = (props) => {
		const { data } = props ?? {};

		return organisationCreate(data, requestOptions);
	};

	return useMutation<
		Awaited<ReturnType<typeof organisationCreate>>,
		TError,
		{ data: CreateOrganisationRequest },
		TContext
	>(mutationFn, mutationOptions);
};
/**
 * @summary Gets a list of all Organisations
 */
export const organisationsList = (
	params?: OrganisationsListParams,
	options?: SecondParameter<typeof customInstance>,
	signal?: AbortSignal
) => {
	return customInstance<OrganisationRecord[]>(
		{ url: `/api/v1/organisations`, method: 'get', signal, params },
		options
	);
};

export const getOrganisationsListQueryKey = (
	params?: OrganisationsListParams
) => [`/api/v1/organisations`, ...(params ? [params] : [])];

export type OrganisationsListQueryResult = NonNullable<
	Awaited<ReturnType<typeof organisationsList>>
>;
export type OrganisationsListQueryError = ErrorType<void>;

export const useOrganisationsList = <
	TData = Awaited<ReturnType<typeof organisationsList>>,
	TError = ErrorType<void>
>(
	params?: OrganisationsListParams,
	options?: {
		query?: UseQueryOptions<
			Awaited<ReturnType<typeof organisationsList>>,
			TError,
			TData
		>;
		request?: SecondParameter<typeof customInstance>;
	}
): UseQueryResult<TData, TError> & { queryKey: QueryKey } => {
	const { query: queryOptions, request: requestOptions } = options ?? {};

	const queryKey =
		queryOptions?.queryKey ?? getOrganisationsListQueryKey(params);

	const queryFn: QueryFunction<
		Awaited<ReturnType<typeof organisationsList>>
	> = ({ signal }) => organisationsList(params, requestOptions, signal);

	const query = useQuery<
		Awaited<ReturnType<typeof organisationsList>>,
		TError,
		TData
	>(queryKey, queryFn, queryOptions);

	return {
		queryKey,
		...query,
	};
};

/**
 * Deletes a Organisation
 * @summary Deletes a Organisation
 */
export const organisationsDelete = (
	organisationRouteName: string,
	options?: SecondParameter<typeof customInstance>
) => {
	return customInstance<void>(
		{
			url: `/api/v1/organisations/${organisationRouteName}`,
			method: 'delete',
		},
		options
	);
};

export type OrganisationsDeleteMutationResult = NonNullable<
	Awaited<ReturnType<typeof organisationsDelete>>
>;

export type OrganisationsDeleteMutationError = ErrorType<unknown>;

export const useOrganisationsDelete = <
	TError = ErrorType<unknown>,
	TContext = unknown
>(options?: {
	mutation?: UseMutationOptions<
		Awaited<ReturnType<typeof organisationsDelete>>,
		TError,
		{ organisationRouteName: string },
		TContext
	>;
	request?: SecondParameter<typeof customInstance>;
}) => {
	const { mutation: mutationOptions, request: requestOptions } =
		options ?? {};

	const mutationFn: MutationFunction<
		Awaited<ReturnType<typeof organisationsDelete>>,
		{ organisationRouteName: string }
	> = (props) => {
		const { organisationRouteName } = props ?? {};

		return organisationsDelete(organisationRouteName, requestOptions);
	};

	return useMutation<
		Awaited<ReturnType<typeof organisationsDelete>>,
		TError,
		{ organisationRouteName: string },
		TContext
	>(mutationFn, mutationOptions);
};
/**
 * Gets a single Organisation by its route name
 * @summary Gets a single Organisation
 */
export const organisationsGetByRouteName = (
	organisationRouteName: string,
	options?: SecondParameter<typeof customInstance>,
	signal?: AbortSignal
) => {
	return customInstance<OrganisationRecord>(
		{
			url: `/api/v1/organisations/${organisationRouteName}`,
			method: 'get',
			signal,
		},
		options
	);
};

export const getOrganisationsGetByRouteNameQueryKey = (
	organisationRouteName: string
) => [`/api/v1/organisations/${organisationRouteName}`];

export type OrganisationsGetByRouteNameQueryResult = NonNullable<
	Awaited<ReturnType<typeof organisationsGetByRouteName>>
>;
export type OrganisationsGetByRouteNameQueryError = ErrorType<void>;

export const useOrganisationsGetByRouteName = <
	TData = Awaited<ReturnType<typeof organisationsGetByRouteName>>,
	TError = ErrorType<void>
>(
	organisationRouteName: string,
	options?: {
		query?: UseQueryOptions<
			Awaited<ReturnType<typeof organisationsGetByRouteName>>,
			TError,
			TData
		>;
		request?: SecondParameter<typeof customInstance>;
	}
): UseQueryResult<TData, TError> & { queryKey: QueryKey } => {
	const { query: queryOptions, request: requestOptions } = options ?? {};

	const queryKey =
		queryOptions?.queryKey ??
		getOrganisationsGetByRouteNameQueryKey(organisationRouteName);

	const queryFn: QueryFunction<
		Awaited<ReturnType<typeof organisationsGetByRouteName>>
	> = ({ signal }) =>
		organisationsGetByRouteName(
			organisationRouteName,
			requestOptions,
			signal
		);

	const query = useQuery<
		Awaited<ReturnType<typeof organisationsGetByRouteName>>,
		TError,
		TData
	>(queryKey, queryFn, { enabled: !!organisationRouteName, ...queryOptions });

	return {
		queryKey,
		...query,
	};
};

/**
 * Gets a single Organisation by its route name
 * @summary Gets a single Organisation
 */
export const organisationsGetById = (
	organisationId: number,
	options?: SecondParameter<typeof customInstance>,
	signal?: AbortSignal
) => {
	return customInstance<OrganisationRecord>(
		{
			url: `/api/v1/organisations/${organisationId}`,
			method: 'get',
			signal,
		},
		options
	);
};

export const getOrganisationsGetByIdQueryKey = (organisationId: number) => [
	`/api/v1/organisations/${organisationId}`,
];

export type OrganisationsGetByIdQueryResult = NonNullable<
	Awaited<ReturnType<typeof organisationsGetById>>
>;
export type OrganisationsGetByIdQueryError = ErrorType<void>;

export const useOrganisationsGetById = <
	TData = Awaited<ReturnType<typeof organisationsGetById>>,
	TError = ErrorType<void>
>(
	organisationId: number,
	options?: {
		query?: UseQueryOptions<
			Awaited<ReturnType<typeof organisationsGetById>>,
			TError,
			TData
		>;
		request?: SecondParameter<typeof customInstance>;
	}
): UseQueryResult<TData, TError> & { queryKey: QueryKey } => {
	const { query: queryOptions, request: requestOptions } = options ?? {};

	const queryKey =
		queryOptions?.queryKey ??
		getOrganisationsGetByIdQueryKey(organisationId);

	const queryFn: QueryFunction<
		Awaited<ReturnType<typeof organisationsGetById>>
	> = ({ signal }) =>
		organisationsGetById(organisationId, requestOptions, signal);

	const query = useQuery<
		Awaited<ReturnType<typeof organisationsGetById>>,
		TError,
		TData
	>(queryKey, queryFn, { enabled: !!organisationId, ...queryOptions });

	return {
		queryKey,
		...query,
	};
};

/**
 * @summary Get all Organisations the current user is a member of
 */
export const organisationsMe = (
	options?: SecondParameter<typeof customInstance>,
	signal?: AbortSignal
) => {
	return customInstance<OrganisationRecord[]>(
		{ url: `/api/v1/organisations/me`, method: 'get', signal },
		options
	);
};

export const getOrganisationsMeQueryKey = () => [`/api/v1/organisations/me`];

export type OrganisationsMeQueryResult = NonNullable<
	Awaited<ReturnType<typeof organisationsMe>>
>;
export type OrganisationsMeQueryError = ErrorType<void>;

export const useOrganisationsMe = <
	TData = Awaited<ReturnType<typeof organisationsMe>>,
	TError = ErrorType<void>
>(options?: {
	query?: UseQueryOptions<
		Awaited<ReturnType<typeof organisationsMe>>,
		TError,
		TData
	>;
	request?: SecondParameter<typeof customInstance>;
}): UseQueryResult<TData, TError> & { queryKey: QueryKey } => {
	const { query: queryOptions, request: requestOptions } = options ?? {};

	const queryKey = queryOptions?.queryKey ?? getOrganisationsMeQueryKey();

	const queryFn: QueryFunction<
		Awaited<ReturnType<typeof organisationsMe>>
	> = ({ signal }) => organisationsMe(requestOptions, signal);

	const query = useQuery<
		Awaited<ReturnType<typeof organisationsMe>>,
		TError,
		TData
	>(queryKey, queryFn, queryOptions);

	return {
		queryKey,
		...query,
	};
};

/**
 * @summary Get all service roles by their id
 */
export const organisationListServiceRoles = (
	organisationRouteName: string,
	options?: SecondParameter<typeof customInstance>,
	signal?: AbortSignal
) => {
	return customInstance<ServiceRoleRecord[]>(
		{
			url: `/api/v1/organisations/${organisationRouteName}/service-roles`,
			method: 'get',
			signal,
		},
		options
	);
};

export const getOrganisationListServiceRolesQueryKey = (
	organisationRouteName: string
) => [`/api/v1/organisations/${organisationRouteName}/service-roles`];

export type OrganisationListServiceRolesQueryResult = NonNullable<
	Awaited<ReturnType<typeof organisationListServiceRoles>>
>;
export type OrganisationListServiceRolesQueryError = ErrorType<void>;

export const useOrganisationListServiceRoles = <
	TData = Awaited<ReturnType<typeof organisationListServiceRoles>>,
	TError = ErrorType<void>
>(
	organisationRouteName: string,
	options?: {
		query?: UseQueryOptions<
			Awaited<ReturnType<typeof organisationListServiceRoles>>,
			TError,
			TData
		>;
		request?: SecondParameter<typeof customInstance>;
	}
): UseQueryResult<TData, TError> & { queryKey: QueryKey } => {
	const { query: queryOptions, request: requestOptions } = options ?? {};

	const queryKey =
		queryOptions?.queryKey ??
		getOrganisationListServiceRolesQueryKey(organisationRouteName);

	const queryFn: QueryFunction<
		Awaited<ReturnType<typeof organisationListServiceRoles>>
	> = ({ signal }) =>
		organisationListServiceRoles(
			organisationRouteName,
			requestOptions,
			signal
		);

	const query = useQuery<
		Awaited<ReturnType<typeof organisationListServiceRoles>>,
		TError,
		TData
	>(queryKey, queryFn, { enabled: !!organisationRouteName, ...queryOptions });

	return {
		queryKey,
		...query,
	};
};

/**
 * List all services
 * @summary List all services
 */
export const organisationListServices = (
	organisationRouteName: string,
	options?: SecondParameter<typeof customInstance>,
	signal?: AbortSignal
) => {
	return customInstance<ServiceRecord[]>(
		{
			url: `/api/v1/organisations/${organisationRouteName}/services`,
			method: 'get',
			signal,
		},
		options
	);
};

export const getOrganisationListServicesQueryKey = (
	organisationRouteName: string
) => [`/api/v1/organisations/${organisationRouteName}/services`];

export type OrganisationListServicesQueryResult = NonNullable<
	Awaited<ReturnType<typeof organisationListServices>>
>;
export type OrganisationListServicesQueryError = ErrorType<void>;

export const useOrganisationListServices = <
	TData = Awaited<ReturnType<typeof organisationListServices>>,
	TError = ErrorType<void>
>(
	organisationRouteName: string,
	options?: {
		query?: UseQueryOptions<
			Awaited<ReturnType<typeof organisationListServices>>,
			TError,
			TData
		>;
		request?: SecondParameter<typeof customInstance>;
	}
): UseQueryResult<TData, TError> & { queryKey: QueryKey } => {
	const { query: queryOptions, request: requestOptions } = options ?? {};

	const queryKey =
		queryOptions?.queryKey ??
		getOrganisationListServicesQueryKey(organisationRouteName);

	const queryFn: QueryFunction<
		Awaited<ReturnType<typeof organisationListServices>>
	> = ({ signal }) =>
		organisationListServices(organisationRouteName, requestOptions, signal);

	const query = useQuery<
		Awaited<ReturnType<typeof organisationListServices>>,
		TError,
		TData
	>(queryKey, queryFn, { enabled: !!organisationRouteName, ...queryOptions });

	return {
		queryKey,
		...query,
	};
};
