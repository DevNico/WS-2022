import React from 'react';
import { MetadataArrayElement } from '../../../api/models';
import { Checkbox, FormControlLabel, Stack, TextField } from '@mui/material';

interface Formik {
	values: Record<string, any>;
	errors: Record<string, any>;
	touched: Record<string, any>;
	handleChange: (event: React.ChangeEvent<any>) => void;
}

interface FormElementProps {
	item: MetadataArrayElement;
	formik: Formik;
	disabled?: boolean;
}

const FormElement: React.FC<FormElementProps> = ({
	item,
	formik,
	disabled,
}) => {
	switch (item.type) {
		case 'checkbox':
			return (
				<FormControlLabel
					control={
						<Checkbox
							checked={formik.values[item.name!]}
							onChange={formik.handleChange}
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
					id={item.name!}
					name={item.name!}
					fullWidth
					value={formik.values[item.name!]}
					onChange={formik.handleChange}
					error={
						formik.touched[item.name!] &&
						Boolean(formik.errors[item.name!])
					}
					helperText={
						formik.touched[item.name!] && formik.errors[item.name!]
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
}

const GeneratedServiceTemplateForm: React.FC<
	GeneratedServiceTemplateFormProps
> = ({ template, formik, disabled }) => {
	return (
		<>
			{template.map((item, index) => (
				<FormElement
					formik={formik}
					key={index}
					item={item}
					disabled={disabled}
				/>
			))}
		</>
	);
};

export default GeneratedServiceTemplateForm;
