import React from 'react';
import DialogTitle from '@mui/material/DialogTitle/DialogTitle';
import DialogContent from '@mui/material/DialogContent/DialogContent';
import Dialog from '@mui/material/Dialog/Dialog';
import { MetadataArrayElement } from '../../../api/models';
import { useTranslation } from 'react-i18next';
import GeneratedServiceTemplateForm from './GeneratedServiceTemplateForm';
import { useFormik } from 'formik';
import { Button, Stack } from '@mui/material';
import {
	createYupSchemaFromServiceTemplateMetadata,
	formikDefaultValuesFromServiceTemplateMetadata,
	formikTouchedFromServiceTemplateMetadata,
} from '../../../common/serviceTemplateMetadataUtil';

export interface CreateReleaseDialogProps {
	metadata: MetadataArrayElement[];
	open: boolean;
	onClose: (values?: object) => void;
	title: string;
}

const MetadataEditorDialog: React.FC<CreateReleaseDialogProps> = ({
	metadata,
	open,
	onClose,
	title,
}) => {
	const { t } = useTranslation();

	const formik = useFormik({
		initialValues: formikDefaultValuesFromServiceTemplateMetadata(metadata),
		validationSchema: createYupSchemaFromServiceTemplateMetadata(metadata),
		onSubmit: (values) => {
			onClose(values);
		},
	});

	return (
		<Dialog
			open={open}
			onClose={() => {
				formik.resetForm();
				onClose();
			}}
		>
			<DialogTitle>{title}</DialogTitle>
			<DialogContent>
				<form onSubmit={formik.handleSubmit}>
					<Stack
						spacing={2}
						sx={{ margin: '10px 0' }}
						justifyContent='center'
						alignItems='center'
					>
						<GeneratedServiceTemplateForm
							template={metadata}
							formik={formik}
							prefix=''
						/>
						<Button type='submit' variant='contained'>
							{t('common.submit')}
						</Button>
					</Stack>
				</form>
			</DialogContent>
		</Dialog>
	);
};

export default MetadataEditorDialog;
