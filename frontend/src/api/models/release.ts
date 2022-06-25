/**
 * Generated by orval v6.8.1 🍺
 * Do not edit manually.
 * Service Release Manager API
 * OpenAPI spec version: v1
 */
import type { DomainEventBase } from './domainEventBase';
import type { OrganisationUser } from './organisationUser';
import type { ReleaseLocalisedMetadata } from './releaseLocalisedMetadata';

export interface Release {
	id?: number;
	updatedAt?: string;
	createdAt?: string;
	readonly domainEvents?: DomainEventBase[] | null;
	approvedBy?: OrganisationUser;
	readonly approvedAt?: string | null;
	publishedBy?: OrganisationUser;
	readonly publishedAt?: string | null;
	version: string;
	metadata: string;
	localisedMetadataList?: ReleaseLocalisedMetadata[] | null;
	serviceId?: number;
}
