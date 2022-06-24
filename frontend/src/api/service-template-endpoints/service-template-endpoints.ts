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
import type {
	ServiceTemplateRecord,
	ErrorResponse,
	CreateServiceTemplate,
	UpdateServiceTemplate,
} from '.././models';
import { customInstance, ErrorType } from '.././axios';

// eslint-disable-next-line
type SecondParameter<T extends (...args: any) => any> = T extends (
	config: any,
	args: infer P
) => any
	? P
	: never;

/**
 * Create a new service template
 * @summary Add a new service template
 */
export const serviceTemplateCreate = (
	createServiceTemplate: CreateServiceTemplate,
	options?: SecondParameter<typeof customInstance>
) => {
	return customInstance<ServiceTemplateRecord>(
		{
			url: `/api/v1/service-templates`,
			method: 'post',
			headers: { 'Content-Type': 'application/json' },
			data: createServiceTemplate,
		},
		options
	);
};

export type ServiceTemplateCreateMutationResult = NonNullable<
	Awaited<ReturnType<typeof serviceTemplateCreate>>
>;
export type ServiceTemplateCreateMutationBody = CreateServiceTemplate;
export type ServiceTemplateCreateMutationError =
	ErrorType<ErrorResponse | void>;

export const useServiceTemplateCreate = <
	TError = ErrorType<ErrorResponse | void>,
	TContext = unknown
>(options?: {
	mutation?: UseMutationOptions<
		Awaited<ReturnType<typeof serviceTemplateCreate>>,
		TError,
		{ data: CreateServiceTemplate },
		TContext
	>;
	request?: SecondParameter<typeof customInstance>;
}) => {
	const { mutation: mutationOptions, request: requestOptions } =
		options ?? {};

	const mutationFn: MutationFunction<
		Awaited<ReturnType<typeof serviceTemplateCreate>>,
		{ data: CreateServiceTemplate }
	> = (props) => {
		const { data } = props ?? {};

		return serviceTemplateCreate(data, requestOptions);
	};

	return useMutation<
		Awaited<ReturnType<typeof serviceTemplateCreate>>,
		TError,
		{ data: CreateServiceTemplate },
		TContext
	>(mutationFn, mutationOptions);
};
/**
 * Deletes a service template by its id
 * @summary Deletes a service template
 */
export const serviceTemplateDelete = (
	options?: SecondParameter<typeof customInstance>
) => {
	return customInstance<void>(
		{ url: `/api/v1/service-templates`, method: 'delete' },
		options
	);
};

export type ServiceTemplateDeleteMutationResult = NonNullable<
	Awaited<ReturnType<typeof serviceTemplateDelete>>
>;

export type ServiceTemplateDeleteMutationError = ErrorType<unknown>;

export const useServiceTemplateDelete = <
	TError = ErrorType<unknown>,
	TVariables = void,
	TContext = unknown
>(options?: {
	mutation?: UseMutationOptions<
		Awaited<ReturnType<typeof serviceTemplateDelete>>,
		TError,
		TVariables,
		TContext
	>;
	request?: SecondParameter<typeof customInstance>;
}) => {
	const { mutation: mutationOptions, request: requestOptions } =
		options ?? {};

	const mutationFn: MutationFunction<
		Awaited<ReturnType<typeof serviceTemplateDelete>>,
		TVariables
	> = () => {
		return serviceTemplateDelete(requestOptions);
	};

	return useMutation<
		Awaited<ReturnType<typeof serviceTemplateDelete>>,
		TError,
		TVariables,
		TContext
	>(mutationFn, mutationOptions);
};
/**
 * List all service templates
 * @summary List all service templates
 */
export const serviceTemplateList = (
	options?: SecondParameter<typeof customInstance>,
	signal?: AbortSignal
) => {
	return customInstance<ServiceTemplateRecord[]>(
		{ url: `/api/v1/service-templates`, method: 'get', signal },
		options
	);
};

export const getServiceTemplateListQueryKey = () => [
	`/api/v1/service-templates`,
];

export type ServiceTemplateListQueryResult = NonNullable<
	Awaited<ReturnType<typeof serviceTemplateList>>
>;
export type ServiceTemplateListQueryError = ErrorType<void>;

export const useServiceTemplateList = <
	TData = Awaited<ReturnType<typeof serviceTemplateList>>,
	TError = ErrorType<void>
>(options?: {
	query?: UseQueryOptions<
		Awaited<ReturnType<typeof serviceTemplateList>>,
		TError,
		TData
	>;
	request?: SecondParameter<typeof customInstance>;
}): UseQueryResult<TData, TError> & { queryKey: QueryKey } => {
	const { query: queryOptions, request: requestOptions } = options ?? {};

	const queryKey = queryOptions?.queryKey ?? getServiceTemplateListQueryKey();

	const queryFn: QueryFunction<
		Awaited<ReturnType<typeof serviceTemplateList>>
	> = ({ signal }) => serviceTemplateList(requestOptions, signal);

	const query = useQuery<
		Awaited<ReturnType<typeof serviceTemplateList>>,
		TError,
		TData
	>(queryKey, queryFn, queryOptions);

	return {
		queryKey,
		...query,
	};
};

/**
 * Update a service template
 * @summary Update a service template
 */
export const serviceTemplateUpdate = (
	updateServiceTemplate: UpdateServiceTemplate,
	options?: SecondParameter<typeof customInstance>
) => {
	return customInstance<ServiceTemplateRecord>(
		{
			url: `/api/v1/service-templates`,
			method: 'patch',
			headers: { 'Content-Type': 'application/json' },
			data: updateServiceTemplate,
		},
		options
	);
};

export type ServiceTemplateUpdateMutationResult = NonNullable<
	Awaited<ReturnType<typeof serviceTemplateUpdate>>
>;
export type ServiceTemplateUpdateMutationBody = UpdateServiceTemplate;
export type ServiceTemplateUpdateMutationError =
	ErrorType<ErrorResponse | void>;

export const useServiceTemplateUpdate = <
	TError = ErrorType<ErrorResponse | void>,
	TContext = unknown
>(options?: {
	mutation?: UseMutationOptions<
		Awaited<ReturnType<typeof serviceTemplateUpdate>>,
		TError,
		{ data: UpdateServiceTemplate },
		TContext
	>;
	request?: SecondParameter<typeof customInstance>;
}) => {
	const { mutation: mutationOptions, request: requestOptions } =
		options ?? {};

	const mutationFn: MutationFunction<
		Awaited<ReturnType<typeof serviceTemplateUpdate>>,
		{ data: UpdateServiceTemplate }
	> = (props) => {
		const { data } = props ?? {};

		return serviceTemplateUpdate(data, requestOptions);
	};

	return useMutation<
		Awaited<ReturnType<typeof serviceTemplateUpdate>>,
		TError,
		{ data: UpdateServiceTemplate },
		TContext
	>(mutationFn, mutationOptions);
};
/**
 * Get a service template by its name
 * @summary Get a service template
 */
export const serviceTemplateGet = (
	serviceTemplateName: string,
	options?: SecondParameter<typeof customInstance>,
	signal?: AbortSignal
) => {
	return customInstance<ServiceTemplateRecord>(
		{
			url: `/api/v1/service-templates/${serviceTemplateName}`,
			method: 'get',
			signal,
		},
		options
	);
};

export const getServiceTemplateGetQueryKey = (serviceTemplateName: string) => [
	`/api/v1/service-templates/${serviceTemplateName}`,
];

export type ServiceTemplateGetQueryResult = NonNullable<
	Awaited<ReturnType<typeof serviceTemplateGet>>
>;
export type ServiceTemplateGetQueryError = ErrorType<void>;

export const useServiceTemplateGet = <
	TData = Awaited<ReturnType<typeof serviceTemplateGet>>,
	TError = ErrorType<void>
>(
	serviceTemplateName: string,
	options?: {
		query?: UseQueryOptions<
			Awaited<ReturnType<typeof serviceTemplateGet>>,
			TError,
			TData
		>;
		request?: SecondParameter<typeof customInstance>;
	}
): UseQueryResult<TData, TError> & { queryKey: QueryKey } => {
	const { query: queryOptions, request: requestOptions } = options ?? {};

	const queryKey =
		queryOptions?.queryKey ??
		getServiceTemplateGetQueryKey(serviceTemplateName);

	const queryFn: QueryFunction<
		Awaited<ReturnType<typeof serviceTemplateGet>>
	> = ({ signal }) =>
		serviceTemplateGet(serviceTemplateName, requestOptions, signal);

	const query = useQuery<
		Awaited<ReturnType<typeof serviceTemplateGet>>,
		TError,
		TData
	>(queryKey, queryFn, { enabled: !!serviceTemplateName, ...queryOptions });

	return {
		queryKey,
		...query,
	};
};
