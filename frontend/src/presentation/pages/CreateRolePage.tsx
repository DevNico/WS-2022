import React, { useEffect, useRef } from 'react';
import Center from '../components/Center';
import { useFormik } from 'formik';
import {
	Checkbox,
	FormControl,
	FormControlLabel,
	InputLabel,
	MenuItem,
	Select,
	Stack,
	TextField,
	Typography,
} from '@mui/material';
import * as yup from 'yup';
import { useTranslation } from 'react-i18next';
import { useMutation } from 'react-query';
import { organisationRoleCreate } from '../../api/organisation-role/organisation-role';
import { organisationsList } from '../../api/organisation/organisation';
import { useKeycloak } from '@react-keycloak/web';
import { useNavigate } from 'react-router-dom';
import CustomAlert from '../components/CustomAlert';
import AlertContainer from '../components/AlertContainer';
import { LoadingButton } from '@mui/lab';
import { checkUserIsSuperAdminEffect } from '../../util';

const CreateRolePage: React.FC = () => {
	const [loading, setLoading] = React.useState(true);
	const createRole = useMutation(organisationRoleCreate);
	const organisationsListMutation = useMutation(organisationsList);
	const successAlert = useRef<CustomAlert>(null);
	const errorAlert = useRef<CustomAlert>(null);

	const { keycloak } = useKeycloak();
	const navigate = useNavigate();
	const { t } = useTranslation();

	const errorMessage =
		(createRole.error as any)?.message || 'No message available';

	useEffect(
		checkUserIsSuperAdminEffect.bind(
			null,
			keycloak,
			navigate,
			setLoading,
			() => organisationsListMutation.mutateAsync(undefined)
		),
		[]
	);

	const validationSchema = yup.object({
		name: yup.string().min(2).max(50).required('Name is required'),
		organisationId: yup.number().required('Organisation is required'),
		serviceWrite: yup.boolean().required('Service write is required'),
		serviceDelete: yup.boolean().required('Service delete is required'),
		userRead: yup.boolean().required('User read is required'),
		userWrite: yup.boolean().required('UserWrite is required'),
		userDelete: yup.boolean().required('User delete is required'),
	});

	const formik = useFormik({
		initialValues: {
			name: '',
			organisationId: '' as unknown as number,
			serviceWrite: false,
			serviceDelete: false,
			userRead: false,
			userWrite: false,
			userDelete: false,
		},
		validationSchema,
		onSubmit: async (values) => {
			setLoading(true);
			try {
				await createRole.mutateAsync(values);
				successAlert.current?.show();
			} catch (e) {
				errorAlert.current?.show();
				throw e;
			} finally {
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
							{t('roles.create.title')}
						</Typography>
						<TextField
							fullWidth
							id='name'
							name='name'
							label={t('roles.create.name')}
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
						<FormControl fullWidth disabled={loading}>
							<InputLabel id='organisation-select-label'>
								{t('roles.create.organisation')}
							</InputLabel>
							<Select
								labelId='organisation-select-label'
								id='organisationId'
								name='organisationId'
								value={formik.values.organisationId + ''}
								onChange={formik.handleChange}
								error={
									formik.touched.organisationId &&
									Boolean(formik.errors.organisationId)
								}
								label={t('roles.create.organisation')}
							>
								{organisationsListMutation.data?.map(
									(organisation) => (
										<MenuItem
											value={organisation.id}
											key={organisation.id}
										>
											{organisation.name}
										</MenuItem>
									)
								)}
							</Select>
						</FormControl>
						<Typography variant='h6'>
							{t('roles.create.permissions')}
						</Typography>
						<FormControl>
							<FormControlLabel
								disabled={loading}
								control={
									<Checkbox
										id='serviceWrite'
										checked={formik.values.serviceWrite}
										onChange={formik.handleChange}
									/>
								}
								label={t('roles.create.serviceWrite')}
							/>
							<FormControlLabel
								disabled={loading}
								control={
									<Checkbox
										id='serviceDelete'
										checked={formik.values.serviceDelete}
										onChange={formik.handleChange}
									/>
								}
								label={t('roles.create.serviceDelete')}
							/>
							<FormControlLabel
								disabled={loading}
								control={
									<Checkbox
										id='userRead'
										checked={formik.values.userRead}
										onChange={formik.handleChange}
									/>
								}
								label={t('roles.create.userRead')}
							/>
							<FormControlLabel
								disabled={loading}
								control={
									<Checkbox
										id='userWrite'
										checked={formik.values.userWrite}
										onChange={formik.handleChange}
									/>
								}
								label={t('roles.create.userWrite')}
							/>
							<FormControlLabel
								disabled={loading}
								control={
									<Checkbox
										id='userDelete'
										checked={formik.values.userDelete}
										onChange={formik.handleChange}
									/>
								}
								label={t('roles.create.userDelete')}
							/>
						</FormControl>
						<LoadingButton
							loading={loading}
							type='submit'
							variant='contained'
						>
							{t('common.submit')}
						</LoadingButton>
					</Stack>
				</form>
			</Center>
			<AlertContainer>
				<CustomAlert severity='success' ref={successAlert}>
					{t('roles.create.success')}
				</CustomAlert>
				<CustomAlert severity='error' ref={errorAlert}>
					{t('roles.create.error', { error: errorMessage })}
				</CustomAlert>
			</AlertContainer>
		</>
	);
};
export default CreateRolePage;
