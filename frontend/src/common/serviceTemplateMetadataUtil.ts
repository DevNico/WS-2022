import { MetadataArrayElement } from '../api/models';
import * as yup from 'yup';
import { BooleanSchema, StringSchema } from 'yup';

export function createYupSchemaFromServiceTemplateMetadata(
	metadata: MetadataArrayElement[]
) {
	const schema: Record<string, StringSchema | BooleanSchema> = {};
	metadata.forEach((element) => {
		let elementSchema: StringSchema | BooleanSchema;
		switch (element.type) {
			case 'email':
				elementSchema = yup.string().email();
				break;
			case 'checkbox':
				elementSchema = yup.boolean();
				break;
			default:
				elementSchema = yup.string();
		}

		if (element.minLength) {
			elementSchema = (elementSchema as StringSchema).min(
				element.minLength
			);
		}

		if (element.maxLength) {
			elementSchema = (elementSchema as StringSchema).max(
				element.maxLength
			);
		}

		if (element.type !== 'checkbox') {
			if (element.required) {
				elementSchema = elementSchema.required();
			} else {
				elementSchema = elementSchema.optional();
			}
		}

		schema[element.name!] = elementSchema;
	});

	return yup.object(schema);
}

export function formikDefaultValuesFromServiceTemplateMetadata(
	metadata: MetadataArrayElement[]
): Record<string, string | boolean> {
	const defaultValues: Record<string, string | boolean> = {};
	metadata.forEach((element) => {
		if (element.type !== 'checkbox') {
			defaultValues[element.name!] = '';
		} else {
			defaultValues[element.name!] = false;
		}
	});

	return defaultValues;
}
