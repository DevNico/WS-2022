import LoadingButton from '@mui/lab/LoadingButton';
import MenuItem from '@mui/material/MenuItem';
import Stack from '@mui/material/Stack';
import TextField from '@mui/material/TextField';
import { useFormik } from 'formik';
import React from 'react';
import { toast } from 'react-hot-toast';
import { useTranslation } from 'react-i18next';
import { useMutation, useQueryClient } from 'react-query';
import * as yup from 'yup';
import { CreateServiceRequest, OrganisationRecord } from '../../../api/models';
import { serviceCreate } from '../../../api/service/service';
import {
	getOrganisationListServicesQueryKey,
	useOrganisationListServiceTemplates,
} from '../../../api/organisation/organisation';

export interface ServiceFormProps {
	onSubmitSuccess: () => void;
	organisation: OrganisationRecord;
}

const ServiceForm: React.FC<ServiceFormProps> = ({
	onSubmitSuccess,
	organisation,
}) => {
	const { t } = useTranslation();

	const queryClient = useQueryClient();
	const createService = useMutation(serviceCreate);
	const serviceTemplate = useOrganisationListServiceTemplates(
		organisation.routeName!
	);

	const validationSchema = yup.object({
		name: yup.string().min(5).max(50).required('Name is required'),
		description: yup
			.string()
			.min(5)
			.max(250)
			.required('Description is required'),
	});

	const formik = useFormik<CreateServiceRequest>({
		initialValues: {
			name: '',
			description: '',
			serviceTemplateId: -1,
		},
		validationSchema,
		onSubmit: async (values) => {
			await toast.promise(createService.mutateAsync(values), {
				loading: t('common.loading'),
				success: () => {
					formik.resetForm();
					return t('service.create.success');
				},
				error: t('service.create.error', {
					error:
						(createService.error as any)?.message ||
						'No message available',
				}),
			});
			await queryClient.invalidateQueries(
				getOrganisationListServicesQueryKey(organisation.routeName!)
			);
			onSubmitSuccess();
		},
	});

	return (
		<form onSubmit={formik.handleSubmit}>
			<Stack
				spacing={2}
				justifyContent='center'
				alignItems='center'
				mt={1}
			>
				<TextField
					fullWidth
					id='name'
					name='name'
					label={t('service.model.name')}
					value={formik.values.name}
					onChange={formik.handleChange}
					error={formik.touched.name && Boolean(formik.errors.name)}
					helperText={formik.touched.name && formik.errors.name}
					disabled={createService.isLoading}
				/>
				<TextField
					fullWidth
					id='description'
					name='description'
					label={t('service.model.description')}
					value={formik.values.description}
					onChange={formik.handleChange}
					error={
						formik.touched.description &&
						Boolean(formik.errors.description)
					}
					helperText={
						formik.touched.description && formik.errors.description
					}
					multiline
					rows={3}
					disabled={createService.isLoading}
				/>
				<TextField
					fullWidth
					select
					id='serviceTemplateId'
					name='serviceTemplateId'
					label={t('service.model.template')}
					value={
						formik.values.serviceTemplateId === -1
							? ''
							: formik.values.serviceTemplateId
					}
					onChange={formik.handleChange}
					error={
						formik.touched.serviceTemplateId &&
						Boolean(formik.errors.serviceTemplateId)
					}
					helperText={
						formik.touched.serviceTemplateId &&
						formik.errors.serviceTemplateId
					}
					disabled={serviceTemplate.isLoading}
				>
					{serviceTemplate.data?.map((template) => (
						<MenuItem value={template.id} key={template.id}>
							{template.name}
						</MenuItem>
					))}
				</TextField>
				<LoadingButton
					type='submit'
					loading={createService.isLoading}
					variant='contained'
				>
					{t('service.create.submit')}
				</LoadingButton>
			</Stack>
		</form>
	);
};

export default ServiceForm;
