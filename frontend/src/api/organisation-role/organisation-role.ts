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
	CreateOrganisationRoleRequest,
	OrganisationRoleRecord,
} from '.././models';

// eslint-disable-next-line
type SecondParameter<T extends (...args: any) => any> = T extends (
	config: any,
	args: infer P
) => any
	? P
	: never;

/**
 * Creates a new OrganisationRole
 * @summary Creates a new OrganisationRole
 */
export const organisationRoleCreate = (
	createOrganisationRoleRequest: CreateOrganisationRoleRequest,
	options?: SecondParameter<typeof customInstance>
) => {
	return customInstance<OrganisationRoleRecord>(
		{
			url: `/api/v1/organisation-roles`,
			method: 'post',
			headers: { 'Content-Type': 'application/json' },
			data: createOrganisationRoleRequest,
		},
		options
	);
};

export type OrganisationRoleCreateMutationResult = NonNullable<
	Awaited<ReturnType<typeof organisationRoleCreate>>
>;
export type OrganisationRoleCreateMutationBody = CreateOrganisationRoleRequest;
export type OrganisationRoleCreateMutationError = ErrorType<void>;

export const useOrganisationRoleCreate = <
	TError = ErrorType<void>,
	TContext = unknown
>(options?: {
	mutation?: UseMutationOptions<
		Awaited<ReturnType<typeof organisationRoleCreate>>,
		TError,
		{ data: CreateOrganisationRoleRequest },
		TContext
	>;
	request?: SecondParameter<typeof customInstance>;
}) => {
	const { mutation: mutationOptions, request: requestOptions } =
		options ?? {};

	const mutationFn: MutationFunction<
		Awaited<ReturnType<typeof organisationRoleCreate>>,
		{ data: CreateOrganisationRoleRequest }
	> = (props) => {
		const { data } = props ?? {};

		return organisationRoleCreate(data, requestOptions);
	};

	return useMutation<
		Awaited<ReturnType<typeof organisationRoleCreate>>,
		TError,
		{ data: CreateOrganisationRoleRequest },
		TContext
	>(mutationFn, mutationOptions);
};
/**
 * Deletes a OrganisationRole
 * @summary Deletes a OrganisationRole
 */
export const organisationRolesDelete = (
	organisationRoleId: number,
	options?: SecondParameter<typeof customInstance>
) => {
	return customInstance<void>(
		{
			url: `/api/v1/organisation-roles/${organisationRoleId}`,
			method: 'delete',
		},
		options
	);
};

export type OrganisationRolesDeleteMutationResult = NonNullable<
	Awaited<ReturnType<typeof organisationRolesDelete>>
>;

export type OrganisationRolesDeleteMutationError = ErrorType<unknown>;

export const useOrganisationRolesDelete = <
	TError = ErrorType<unknown>,
	TContext = unknown
>(options?: {
	mutation?: UseMutationOptions<
		Awaited<ReturnType<typeof organisationRolesDelete>>,
		TError,
		{ organisationRoleId: number },
		TContext
	>;
	request?: SecondParameter<typeof customInstance>;
}) => {
	const { mutation: mutationOptions, request: requestOptions } =
		options ?? {};

	const mutationFn: MutationFunction<
		Awaited<ReturnType<typeof organisationRolesDelete>>,
		{ organisationRoleId: number }
	> = (props) => {
		const { organisationRoleId } = props ?? {};

		return organisationRolesDelete(organisationRoleId, requestOptions);
	};

	return useMutation<
		Awaited<ReturnType<typeof organisationRolesDelete>>,
		TError,
		{ organisationRoleId: number },
		TContext
	>(mutationFn, mutationOptions);
};
/**
 * @summary Gets a list of all OrganisationRoles
 */
export const organisationRolesList = (
	organisationRouteName: string,
	options?: SecondParameter<typeof customInstance>,
	signal?: AbortSignal
) => {
	return customInstance<OrganisationRoleRecord[]>(
		{
			url: `/api/v1/organisations/${organisationRouteName}/roles`,
			method: 'get',
			signal,
		},
		options
	);
};

export const getOrganisationRolesListQueryKey = (
	organisationRouteName: string
) => [`/api/v1/organisations/${organisationRouteName}/roles`];

export type OrganisationRolesListQueryResult = NonNullable<
	Awaited<ReturnType<typeof organisationRolesList>>
>;
export type OrganisationRolesListQueryError = ErrorType<void>;

export const useOrganisationRolesList = <
	TData = Awaited<ReturnType<typeof organisationRolesList>>,
	TError = ErrorType<void>
>(
	organisationRouteName: string,
	options?: {
		query?: UseQueryOptions<
			Awaited<ReturnType<typeof organisationRolesList>>,
			TError,
			TData
		>;
		request?: SecondParameter<typeof customInstance>;
	}
): UseQueryResult<TData, TError> & { queryKey: QueryKey } => {
	const { query: queryOptions, request: requestOptions } = options ?? {};

	const queryKey =
		queryOptions?.queryKey ??
		getOrganisationRolesListQueryKey(organisationRouteName);

	const queryFn: QueryFunction<
		Awaited<ReturnType<typeof organisationRolesList>>
	> = ({ signal }) =>
		organisationRolesList(organisationRouteName, requestOptions, signal);

	const query = useQuery<
		Awaited<ReturnType<typeof organisationRolesList>>,
		TError,
		TData
	>(queryKey, queryFn, { enabled: !!organisationRouteName, ...queryOptions });

	return {
		queryKey,
		...query,
	};
};
