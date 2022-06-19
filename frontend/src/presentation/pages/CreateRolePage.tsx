import React from 'react';
import Center from '../components/Center';
import { useFormik } from 'formik';
import {
	Checkbox,
	FormControl,
	FormControlLabel,
	Stack,
	TextField,
	Typography,
} from '@mui/material';
import * as yup from 'yup';
import { useTranslation } from 'react-i18next';
import { useMutation } from 'react-query';
import { organisationRoleCreate } from '../../api/organisation-role/organisation-role';
import { useOrganisationsGetByName } from '../../api/organisation/organisation';
import { useNavigate, useParams } from 'react-router-dom';
import { LoadingButton } from '@mui/lab';
import { isSuperAdminState } from '../../util';
import { toast } from 'react-hot-toast';
import { useRecoilValue } from 'recoil';

const CreateRolePage: React.FC = () => {
	const { name } = useParams();
	const navigate = useNavigate();
	const { t } = useTranslation();

	const isSuperAdmin = useRecoilValue(isSuperAdminState);
	const createRole = useMutation(organisationRoleCreate);
	const organisationByName = useOrganisationsGetByName(name!);
	const loading = organisationByName.isLoading || createRole.isLoading;

	if (!isSuperAdmin || organisationByName.error) {
		navigate('/notFound');
		return <></>;
	}

	const validationSchema = yup.object({
		name: yup.string().min(5).max(50).required('Name is required'),
		serviceWrite: yup.boolean().required('Service write is required'),
		serviceDelete: yup.boolean().required('Service delete is required'),
		userRead: yup.boolean().required('User read is required'),
		userWrite: yup.boolean().required('UserWrite is required'),
		userDelete: yup.boolean().required('User delete is required'),
	});

	const formik = useFormik({
		initialValues: {
			name: '',
			serviceWrite: false,
			serviceDelete: false,
			userRead: false,
			userWrite: false,
			userDelete: false,
		},
		validationSchema,
		onSubmit: (values) => {
			return toast.promise(
				createRole.mutateAsync({
					...values,
					organisationId: organisationByName.data?.id!,
				}),
				{
					loading: t('common.loading'),
					success: () => {
						formik.resetForm();
						return t('roles.create.success');
					},
					error: t('roles.create.error', {
						error:
							(createRole.error as any)?.message ||
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
							formik.touched.name && Boolean(formik.errors.name)
						}
						helperText={formik.touched.name && formik.errors.name}
						disabled={loading}
					/>
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
	);
};
export default CreateRolePage;
