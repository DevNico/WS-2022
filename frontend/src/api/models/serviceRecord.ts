/**
 * Generated by orval v6.8.1 🍺
 * Do not edit manually.
 * Service Release Manager API
 * OpenAPI spec version: v1
 */
import type { ReleaseRecord } from './releaseRecord';

export interface ServiceRecord {
	id?: number;
	name?: string | null;
	routeName?: string | null;
	description?: string | null;
	latestRelease?: ReleaseRecord;
	organisationId?: number;
	updatedAt?: string;
	createdAt?: string;
}
