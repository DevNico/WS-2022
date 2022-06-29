import LoadingButton from '@mui/lab/LoadingButton/LoadingButton';
import Grid from '@mui/material/Grid';
import TextField from '@mui/material/TextField/TextField';
import { useFormik } from 'formik';
import React from 'react';
import { toast } from 'react-hot-toast';
import { useTranslation } from 'react-i18next';
import { useMutation, useQueryClient } from 'react-query';
import * as yup from 'yup';
import { CreateServiceTemplate, OrganisationRecord } from '../../../api/models';
import { useOrganisationRolesList } from '../../../api/organisation-role/organisation-role';
import ServiceTemplateMetadataEditor from './ServiceTemplateMetadataEditor';
import { serviceTemplateCreate } from '../../../api/service-template/service-template';
import { getOrganisationListServiceTemplatesQueryKey } from '../../../api/organisation/organisation';

interface ServiceTemplateFormProps {
	organisation: OrganisationRecord;
	onSubmitSuccess: () => void;
}

const ServiceTemplateForm: React.FC<ServiceTemplateFormProps> = ({
	organisation,
	onSubmitSuccess,
}) => {
	const { t } = useTranslation();

	const queryClient = useQueryClient();
	const createUser = useMutation(serviceTemplateCreate);
	const roles = useOrganisationRolesList(organisation.routeName!);
	const loading = createUser.isLoading || roles.isLoading;

	const validationSchema = yup.object({
		name: yup.string().min(5).max(50).required(),
	});

	type CreateServiceTemplateForm = Omit<
		CreateServiceTemplate,
		'staticMetadata' | 'localizedMetadata'
	> & {
		staticMetadata: string;
		localizedMetadata: string;
	};

	const formik = useFormik<CreateServiceTemplateForm>({
		initialValues: {
			name: '',
			staticMetadata: '[]',
			localizedMetadata: '[]',
			organisationId: organisation.id!,
		},
		validationSchema,
		onSubmit: async (values) => {
			await toast.promise(
				createUser.mutateAsync({
					...values,
					staticMetadata: JSON.parse(values.staticMetadata),
					localizedMetadata: JSON.parse(values.localizedMetadata),
				}),
				{
					loading: t('common.loading'),
					success: () => {
						formik.resetForm();
						return t('serviceTemplate.create.success');
					},
					error: t('serviceTemplate.create.error', {
						error:
							(createUser.error as any)?.message ||
							'No message available',
					}),
				}
			);
			await queryClient.invalidateQueries(
				getOrganisationListServiceTemplatesQueryKey(
					organisation.routeName!
				)
			);
			onSubmitSuccess();
		},
	});

	const handleStaticMetadataChanged = (value: string) => {
		formik.setFieldValue('staticMetadata', value);
	};

	const handleLocalizedMetadataChanged = (value: string) => {
		formik.setFieldValue('localizedMetadata', value);
	};

	return (
		<form onSubmit={formik.handleSubmit}>
			<Grid container spacing={2} justifyContent='center'>
				<Grid item xs={12}>
					<TextField
						fullWidth
						id='name'
						name='name'
						label={t('serviceTemplate.model.name')}
						value={formik.values.name}
						onChange={formik.handleChange}
						error={
							formik.touched.name && Boolean(formik.errors.name)
						}
						helperText={formik.touched.name && formik.errors.name}
						disabled={loading}
					/>
				</Grid>
				<Grid item md={6} sm={12} xs={12}>
					<ServiceTemplateMetadataEditor
						value={formik.values.staticMetadata}
						onChanged={handleStaticMetadataChanged}
						label={t('serviceTemplate.model.staticMetadata')}
					/>
				</Grid>
				<Grid item md={6} sm={12} xs={12}>
					<ServiceTemplateMetadataEditor
						value={formik.values.localizedMetadata}
						onChanged={handleLocalizedMetadataChanged}
						label={t('serviceTemplate.model.localizedMetadata')}
					/>
				</Grid>
				<Grid item>
					<LoadingButton
						type='submit'
						loading={loading}
						variant='contained'
					>
						{t('serviceTemplate.create.submit')}
					</LoadingButton>
				</Grid>
			</Grid>
		</form>
	);
};

export default ServiceTemplateForm;
