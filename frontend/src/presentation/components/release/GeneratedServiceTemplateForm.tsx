import React from 'react';
import { MetadataArrayElement } from '../../../api/models';
import {
	Checkbox,
	FormControlLabel,
	TextField,
	Typography,
} from '@mui/material';
import { useTranslation } from 'react-i18next';

interface Formik {
	values: Record<string, any>;
	errors: Record<string, any>;
	touched: Record<string, any>;
	handleChange: (event: React.ChangeEvent<any>) => void;
}

interface FormElementProps {
	item: MetadataArrayElement;
	formik: Formik;
	prefix: string;
	disabled?: boolean;
}

const FormElement: React.FC<FormElementProps> = ({
	item,
	formik,
	disabled,
	prefix,
}) => {
	const nameWithPrefix = `${prefix}.${item.name}`;
	const v = (value: Record<string, any> | undefined) => (value ? value : {});

	switch (item.type) {
		case 'checkbox':
			return (
				<FormControlLabel
					control={
						<Checkbox
							checked={v(formik.values[prefix])[item.name!]}
							onChange={formik.handleChange}
							name={nameWithPrefix}
						/>
					}
					label={item.label}
					disabled={disabled}
				/>
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
						v(formik.touched[prefix])[item.name!] &&
						Boolean(v(formik.errors[prefix])[item.name!])
					}
					helperText={
						v(formik.touched[prefix])[item.name!] &&
						v(formik.errors[prefix])[item.name!]
					}
					disabled={disabled}
				/>
			);
	}
};

interface GeneratedServiceTemplateFormProps {
	template: MetadataArrayElement[];
	formik: Formik;
	disabled?: boolean;
	prefix: string;
}

const GeneratedServiceTemplateForm: React.FC<
	GeneratedServiceTemplateFormProps
> = ({ template, formik, disabled, prefix }) => {
	const { t } = useTranslation();

	return (
		<>
			{template.length > 0 ? (
				template.map((item, index) => (
					<FormElement
						formik={formik}
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
