import React from 'react';
import { useOrganisationsGetByName } from '../../api/organisation/organisation';
import Center from '../components/Center';
import {
	FormControl,
	InputLabel,
	MenuItem,
	Select,
	Stack,
	TextField,
	Typography,
} from '@mui/material';
import { useFormik } from 'formik';
import * as yup from 'yup';
import { useTranslation } from 'react-i18next';
import { useMutation } from 'react-query';
import { organisationUserCreate } from '../../api/organisation-user/organisation-user';
import { useOrganisationRolesList } from '../../api/organisation-role/organisation-role';
import { useNavigate, useParams } from 'react-router-dom';
import { LoadingButton } from '@mui/lab';
import { isSuperAdminState } from '../../util';
import { toast } from 'react-hot-toast';
import { useRecoilValue } from 'recoil';

interface CreateUserFormValues {
	email: string;
	firstName: string;
	lastName: string;
	roleId: number;
}

const CreateUserPage: React.FC = () => {
	const { name } = useParams();
	const { t } = useTranslation();
	const navigate = useNavigate();

	const createUser = useMutation(organisationUserCreate);
	const isSuperAdmin = useRecoilValue(isSuperAdminState);
	const organisationByName = useOrganisationsGetByName(name!);
	const roles = useOrganisationRolesList(name!);
	const loading =
		organisationByName.isLoading || createUser.isLoading || roles.isLoading;

	if (!isSuperAdmin || organisationByName.isError) {
		navigate('/notFound');
		return <></>;
	}

	const validationSchema = yup.object({
		email: yup.string().email().required(),
		firstName: yup
			.string()
			.min(5)
			.max(50)
			.required('First name is required'),
		lastName: yup.string().min(5).max(50).required('Last name is required'),
		roleId: yup.number().required('Role is required'),
	});

	const formik = useFormik<CreateUserFormValues>({
		initialValues: {
			email: '',
			firstName: '',
			lastName: '',
			roleId: '' as any,
		},
		validationSchema,
		onSubmit: async (values) => {
			return toast.promise(
				createUser.mutateAsync({
					...values,
					organisationId: organisationByName.data?.id!,
				}),
				{
					loading: t('common.loading'),
					success: () => {
						formik.resetForm();
						return t('users.create.success');
					},
					error: t('users.create.error', {
						error:
							(createUser.error as any)?.message ||
							'No message available',
					}),
				}
			);
		},
	});

	return (
		<Center>
			<form onSubmit={formik.handleSubmit}>
				<Stack spacing={2} justifyContent='center' alignItems='center'>
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
							formik.touched.email && Boolean(formik.errors.email)
						}
						helperText={formik.touched.email && formik.errors.email}
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
						<InputLabel id='role-select-label'>
							{t('users.create.role')}
						</InputLabel>
						<Select
							labelId='role-select-label'
							id='roleId'
							name='roleId'
							disabled={!roles.data || roles.data.length === 0}
							value={formik.values.roleId + ''}
							onChange={formik.handleChange}
							error={
								formik.touched.roleId &&
								Boolean(formik.errors.roleId)
							}
							label={t('users.create.role')}
						>
							{roles.data?.map((role) => (
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
	);
};

export default CreateUserPage;
