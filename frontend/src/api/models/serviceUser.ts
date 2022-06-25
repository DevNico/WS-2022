/**
 * Generated by orval v6.8.1 🍺
 * Do not edit manually.
 * Service Release Manager API
 * OpenAPI spec version: v1
 */
import type { DomainEventBase } from './domainEventBase';
import type { ServiceRole } from './serviceRole';
import type { OrganisationUser } from './organisationUser';

export interface ServiceUser {
	id?: number;
	updatedAt?: string;
	createdAt?: string;
	readonly domainEvents?: DomainEventBase[] | null;
	serviceRole?: ServiceRole;
	serviceRoleId?: number;
	organisationUser?: OrganisationUser;
	organisationUserId?: number;
	serviceId?: number;
}
