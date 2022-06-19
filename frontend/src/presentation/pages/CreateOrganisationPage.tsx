import React, { useEffect, useRef } from 'react';
import { checkUserIsSuperAdminEffect } from '../../util';
import { useTranslation } from 'react-i18next';
import { useKeycloak } from '@react-keycloak/web';
import { useNavigate } from 'react-router-dom';
import Center from '../components/Center';
import * as yup from 'yup';
import { useFormik } from 'formik';
import { Stack, TextField, Typography } from '@mui/material';
import { LoadingButton } from '@mui/lab';
import { useMutation } from 'react-query';
import { organisationCreate } from '../../api/organisation/organisation';
import CustomAlert from '../components/CustomAlert';
import AlertContainer from '../components/AlertContainer';

const CreateOrganisationPage: React.FC = () => {
	const [loading, setLoading] = React.useState(true);
	const successAlert = useRef<CustomAlert>(null);
	const errorAlert = useRef<CustomAlert>(null);
	const { t } = useTranslation();
	const { keycloak } = useKeycloak();
	const navigate = useNavigate();

	const createOrganisation = useMutation(organisationCreate);
	const errorMessage =
		(createOrganisation.error as any)?.message || 'No message available';

	useEffect(
		checkUserIsSuperAdminEffect.bind(
			null,
			keycloak,
			navigate,
			setLoading,
			() => Promise.resolve()
		),
		[]
	);

	const validationSchema = yup.object({
		name: yup.string().min(2).max(50).required('Name is required'),
	});

	const formik = useFormik({
		initialValues: {
			name: '',
		},
		validationSchema,
		onSubmit: async (values, { setSubmitting }) => {
			try {
				setLoading(true);
				await createOrganisation.mutateAsync(values);
				formik.resetForm();
				successAlert.current?.show();
			} catch (e) {
				errorAlert.current?.show();
				throw e;
			} finally {
				setSubmitting(false);
				setLoading(false);
			}
		},
	});

	return (
		<>
			<Center>
				<form onSubmit={formik.handleSubmit}>
					<Stack
						spacing={2}
						justifyContent='center'
						alignItems='center'
					>
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
								formik.touched.name &&
								Boolean(formik.errors.name)
							}
							helperText={
								formik.touched.name && formik.errors.name
							}
							disabled={loading}
						/>
						<LoadingButton
							type='submit'
							loading={loading}
							variant='contained'
						>
							{t('organisations.create.submit')}
						</LoadingButton>
					</Stack>
				</form>
			</Center>
			<AlertContainer>
				<CustomAlert severity='success' ref={successAlert}>
					{t('organisations.create.success')}
				</CustomAlert>
				<CustomAlert severity='error' ref={errorAlert}>
					{t('organisations.create.error', { error: errorMessage })}
				</CustomAlert>
			</AlertContainer>
		</>
	);
};

export default CreateOrganisationPage;
