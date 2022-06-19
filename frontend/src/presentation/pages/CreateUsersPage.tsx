import React, { useEffect, useRef } from 'react';
import { organisationsList } from '../../api/organisation/organisation';
import Center from '../components/Center';
import {
	FormControl,
	InputLabel,
	MenuItem,
	Select,
	SelectChangeEvent,
	Stack,
	TextField,
	Typography,
} from '@mui/material';
import { useFormik } from 'formik';
import * as yup from 'yup';
import { useTranslation } from 'react-i18next';
import { useMutation } from 'react-query';
import { organisationUserCreate } from '../../api/organisation-user/organisation-user';
import { organisationRolesList } from '../../api/organisation-role/organisation-role';
import { OrganisationRoleRecord } from '../../api/models';
import AlertContainer from '../components/AlertContainer';
import CustomAlert from '../components/CustomAlert';
import { useKeycloak } from '@react-keycloak/web';
import { useNavigate } from 'react-router-dom';
import { LoadingButton } from '@mui/lab';
import { checkUserIsSuperAdminEffect } from '../../util';

const CreateUsersPage: React.FC = () => {
	const [loading, setLoading] = React.useState(true);
	const [roles, setRoles] = React.useState<OrganisationRoleRecord[]>([]);
	const successAlert = useRef<CustomAlert>(null);
	const errorAlert = useRef<CustomAlert>(null);
	const organisationsListMutation = useMutation(organisationsList);
	const rolesMutation = useMutation(organisationRolesList);
	const createUser = useMutation(organisationUserCreate);
	const { t } = useTranslation();
	const { keycloak } = useKeycloak();
	const navigate = useNavigate();

	const errorMessage =
		(createUser.error as any)?.message || 'No message available';

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
		email: yup.string().email().required(),
		firstName: yup.string().required('Name is required'),
		lastName: yup.string().required('Name is required'),
		organisationId: yup.number().required('Organisation is required'),
		roleId: yup.number().required('Role is required'),
	});

	const formik = useFormik({
		initialValues: {
			email: '',
			firstName: '',
			lastName: '',
			organisationId: '' as unknown as number,
			roleId: '' as unknown as number,
		},
		validationSchema,
		onSubmit: async (values, { setSubmitting }) => {
			try {
				setLoading(true);
				await createUser.mutateAsync(values);
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

	const onOrganisationIdChange = async (e: SelectChangeEvent) => {
		formik.handleChange(e);
		setRoles([]);
		if (e.target.value) {
			setRoles(await rolesMutation.mutateAsync(e.target.value));
		}
	};

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
							{t('users.create.title')}
						</Typography>
						<TextField
							fullWidth
							id='email'
							name='email'
							label='Email'
							value={formik.values.email}
							onChange={formik.handleChange}
							error={
								formik.touched.email &&
								Boolean(formik.errors.email)
							}
							helperText={
								formik.touched.email && formik.errors.email
							}
							disabled={loading}
						/>
						<Stack direction='row' spacing={2}>
							<TextField
								fullWidth
								id='firstName'
								name='firstName'
								label={t('users.create.firstName')}
								value={formik.values.firstName}
								onChange={formik.handleChange}
								error={
									formik.touched.firstName &&
									Boolean(formik.errors.firstName)
								}
								helperText={
									formik.touched.firstName &&
									formik.errors.firstName
								}
								disabled={loading}
							/>
							<TextField
								fullWidth
								id='lastName'
								name='lastName'
								label={t('users.create.lastName')}
								value={formik.values.lastName}
								onChange={formik.handleChange}
								error={
									formik.touched.lastName &&
									Boolean(formik.errors.lastName)
								}
								helperText={
									formik.touched.lastName &&
									formik.errors.lastName
								}
								disabled={loading}
							/>
						</Stack>
						<FormControl fullWidth disabled={loading}>
							<InputLabel id='organisation-select-label'>
								{t('users.create.organisation')}
							</InputLabel>
							<Select
								labelId='organisation-select-label'
								id='organisationId'
								name='organisationId'
								value={formik.values.organisationId + ''}
								onChange={onOrganisationIdChange}
								error={
									formik.touched.organisationId &&
									Boolean(formik.errors.organisationId)
								}
								label={t('users.create.organisation')}
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
						<FormControl fullWidth disabled={loading}>
							<InputLabel id='role-select-label'>
								{t('users.create.role')}
							</InputLabel>
							<Select
								labelId='role-select-label'
								id='roleId'
								name='roleId'
								disabled={roles.length === 0}
								value={formik.values.roleId + ''}
								onChange={onOrganisationIdChange}
								error={
									formik.touched.roleId &&
									Boolean(formik.errors.roleId)
								}
								label={t('users.create.role')}
							>
								{roles?.map((role) => (
									<MenuItem value={role.id} key={role.id}>
										{role.name}
									</MenuItem>
								))}
							</Select>
						</FormControl>
						<LoadingButton
							type='submit'
							loading={loading}
							variant='contained'
						>
							{t('users.create.submit')}
						</LoadingButton>
					</Stack>
				</form>
			</Center>
			<AlertContainer>
				<CustomAlert severity='success' ref={successAlert}>
					{t('users.create.success')}
				</CustomAlert>
				<CustomAlert severity='error' ref={errorAlert}>
					{t('users.create.error', { error: errorMessage })}
				</CustomAlert>
			</AlertContainer>
		</>
	);
};

export default CreateUsersPage;
