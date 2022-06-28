/**
 * Generated by orval v6.8.1 🍺
 * Do not edit manually.
 * Service Release Manager API
 * OpenAPI spec version: v1
 */
import type { DomainEventBase } from './domainEventBase';
import type { ReleaseTrigger } from './releaseTrigger';

export interface ReleaseTarget {
	id?: number;
	updatedAt?: string;
	createdAt?: string;
	readonly domainEvents?: DomainEventBase[] | null;
	name: string;
	requiresApproval: boolean;
	releaseTriggers?: ReleaseTrigger[] | null;
	serviceId?: number;
}