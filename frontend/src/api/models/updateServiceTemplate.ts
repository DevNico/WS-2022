/**
 * Generated by orval v6.8.1 🍺
 * Do not edit manually.
 * Service Release Manager API
 * OpenAPI spec version: v1
 */
import type { MetadataArrayElement } from './metadataArrayElement';

export interface UpdateServiceTemplate {
	serviceTemplateId?: number;
	name: string;
	staticMetadata: MetadataArrayElement[];
	localizedMetadata: MetadataArrayElement[];
}
