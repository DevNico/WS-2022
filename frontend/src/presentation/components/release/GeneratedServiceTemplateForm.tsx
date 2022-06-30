import React from 'react';
import { MetadataArrayElement } from '../../../api/models';
import {
	Checkbox,
	FormControl,
	FormControlLabel,
	FormHelperText,
	TextField,
	Typography,
} from '@mui/material';
import { useTranslation } from 'react-i18next';
import { useFormikContext } from 'formik';

interface FormElementProps {
	item: MetadataArrayElement;
	prefix: string;
	disabled?: boolean;
}

const FormElement: React.FC<FormElementProps> = ({
	item,
	disabled,
	prefix,
}) => {
	const nameWithPrefix = `${prefix}.${item.name}`;
	const v = (value: Record<string, any> | undefined) => (value ? value : {});
	const formik = useFormikContext<Record<string, any>>();

	switch (item.type) {
		case 'checkbox':
			const handleChange = () => {
				formik.setFieldValue(
					nameWithPrefix,
					!v(formik.values[prefix])[item.name!]
				);
			};

			return (
				<FormControl>
					<FormControlLabel
						onChange={handleChange}
						name={nameWithPrefix}
						id={nameWithPrefix}
						control={
							<Checkbox
								checked={
									v(formik.values[prefix])[item.name!] ??
									false
								}
							/>
						}
						label={item.label}
						disabled={disabled}
					/>
					{item.placeholder && (
						<FormHelperText>{item.placeholder}</FormHelperText>
					)}
				</FormControl>
			);
		default:
			return (
				<TextField
					label={item.label}
					id={nameWithPrefix}
					name={nameWithPrefix}
					value={v(formik.values[prefix])[item.name!] ?? ''}
					onChange={formik.handleChange}
					error={
						v(formik.touched[prefix] as any)[item.name!] &&
						Boolean(v(formik.errors[prefix] as any)[item.name!])
					}
					helperText={
						v(formik.touched[prefix] as any)[item.name!] &&
						v(formik.errors[prefix] as any)[item.name!]
					}
					disabled={disabled}
					placeholder={item.placeholder ?? undefined}
				/>
			);
	}
};

interface GeneratedServiceTemplateFormProps {
	template: MetadataArrayElement[];
	disabled?: boolean;
	prefix: string;
}

const GeneratedServiceTemplateForm: React.FC<
	GeneratedServiceTemplateFormProps
> = ({ template, disabled, prefix }) => {
	const { t } = useTranslation();

	return (
		<>
			{template.length > 0 ? (
				template.map((item, index) => (
					<FormElement
						key={index}
						item={item}
						disabled={disabled}
						prefix={prefix}
					/>
				))
			) : (
				<Typography>{t('release.create.emptyTemplate')}</Typography>
			)}
		</>
	);
};

export default GeneratedServiceTemplateForm;
