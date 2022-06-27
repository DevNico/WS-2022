import { LoadingButton } from '@mui/lab';
import { Stack, TextField } from '@mui/material';
import { useFormik } from 'formik';
import React from 'react';
import { toast } from 'react-hot-toast';
import { useTranslation } from 'react-i18next';
import { useMutation, useQueryClient } from 'react-query';
import * as yup from 'yup';
import { CreateOrganisationRequest } from '../../../api/models';
import {
	getOrganisationsListQueryKey,
	organisationCreate,
} from '../../../api/organisation/organisation';

export interface OrganisationFormProps {
	onSubmitSuccess: () => void;
}

const OrganisationForm: React.FC<OrganisationFormProps> = ({
	onSubmitSuccess,
}) => {
	const { t } = useTranslation();

	const queryClient = useQueryClient();
	const createOrganisation = useMutation(organisationCreate);

	const validationSchema = yup.object({
		name: yup.string().min(5).max(50).required('Name is required'),
	});

	const formik = useFormik<CreateOrganisationRequest>({
		initialValues: {
			name: '',
		},
		validationSchema,
		onSubmit: async (values) => {
			await toast.promise(createOrganisation.mutateAsync(values), {
				loading: t('common.loading'),
				success: () => {
					formik.resetForm();
					return t('organisation.create.success');
				},
				error: t('organisation.create.error', {
					error:
						(createOrganisation.error as any)?.message ||
						'No message available',
				}),
			});
			queryClient.invalidateQueries(getOrganisationsListQueryKey());
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
					label={t('organisation.create.name')}
					value={formik.values.name}
					onChange={formik.handleChange}
					error={formik.touched.name && Boolean(formik.errors.name)}
					helperText={formik.touched.name && formik.errors.name}
					disabled={createOrganisation.isLoading}
				/>
				<LoadingButton
					type='submit'
					loading={createOrganisation.isLoading}
					variant='contained'
				>
					{t('organisation.create.submit')}
				</LoadingButton>
			</Stack>
		</form>
	);
};

export default OrganisationForm;
