import React from 'react';
import { isSuperAdminState } from '../../util';
import { useTranslation } from 'react-i18next';
import { useNavigate } from 'react-router-dom';
import Center from '../components/Center';
import * as yup from 'yup';
import { useFormik } from 'formik';
import { Stack, TextField, Typography } from '@mui/material';
import { LoadingButton } from '@mui/lab';
import { useMutation } from 'react-query';
import { organisationCreate } from '../../api/organisation/organisation';
import { toast } from 'react-hot-toast';
import { useRecoilValue } from 'recoil';

const CreateOrganisationPage: React.FC = () => {
	const { t } = useTranslation();
	const navigate = useNavigate();

	const createOrganisation = useMutation(organisationCreate);
	const isSuperAdmin = useRecoilValue(isSuperAdminState);

	if (!isSuperAdmin) {
		navigate('/notFound');
		return <></>;
	}

	const validationSchema = yup.object({
		name: yup.string().min(5).max(50).required('Name is required'),
	});

	const formik = useFormik({
		initialValues: {
			name: '',
		},
		validationSchema,
		onSubmit: (values) => {
			return toast.promise(createOrganisation.mutateAsync(values), {
				loading: t('common.loading'),
				success: () => {
					formik.resetForm();
					return t('organisations.create.success');
				},
				error: t('organisations.create.error', {
					error:
						(createOrganisation.error as any)?.message ||
						'No message available',
				}),
			});
		},
	});

	return (
		<Center>
			<form onSubmit={formik.handleSubmit}>
				<Stack spacing={2} justifyContent='center' alignItems='center'>
					<Typography variant='h4'>
						{t('organisations.create.title')}
					</Typography>
					<TextField
						fullWidth
						id='name'
						name='name'
						label={t('organisations.create.name')}
						value={formik.values.name}
						onChange={formik.handleChange}
						error={
							formik.touched.name && Boolean(formik.errors.name)
						}
						helperText={formik.touched.name && formik.errors.name}
						disabled={createOrganisation.isLoading}
					/>
					<LoadingButton
						type='submit'
						loading={createOrganisation.isLoading}
						variant='contained'
					>
						{t('organisations.create.submit')}
					</LoadingButton>
				</Stack>
			</form>
		</Center>
	);
};

export default CreateOrganisationPage;
