import LoadingButton from '@mui/lab/LoadingButton/LoadingButton';
import FormControl from '@mui/material/FormControl/FormControl';
import FormHelperText from '@mui/material/FormHelperText/FormHelperText';
import InputLabel from '@mui/material/InputLabel/InputLabel';
import MenuItem from '@mui/material/MenuItem/MenuItem';
import Select from '@mui/material/Select/Select';
import Stack from '@mui/material/Stack/Stack';
import TextField from '@mui/material/TextField/TextField';
import Typography from '@mui/material/Typography/Typography';
import { useFormik } from 'formik';
import React from 'react';
import { toast } from 'react-hot-toast';
import { useTranslation } from 'react-i18next';
import { useMutation, useQueryClient } from 'react-query';
import * as yup from 'yup';
import {
	CreateOrganisationUserRequest,
	OrganisationRecord,
	OrganisationUserRecord,
} from '../../../api/models';
import { useOrganisationRolesList } from '../../../api/organisation-role/organisation-role';
import {
	getOrganisationUserListQueryKey,
	organisationUserCreate,
} from '../../../api/organisation-user/organisation-user';

interface CreateOrganisationUserFormProps {
	organisation: OrganisationRecord;
	onSubmitSuccess: () => void;
}

const OrganisationUserForm: React.FC<CreateOrganisationUserFormProps> = ({
	organisation,
	onSubmitSuccess,
}) => {
	const { t } = useTranslation();

	const queryClient = useQueryClient();
	const createUser = useMutation(organisationUserCreate);
	const roles = useOrganisationRolesList(organisation.routeName!);
	const loading = createUser.isLoading || roles.isLoading;

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

	const formik = useFormik<CreateOrganisationUserRequest>({
		initialValues: {
			organisationId: organisation.id!,
			email: '',
			firstName: '',
			lastName: '',
			roleId: '' as any,
		},
		validationSchema,
		onSubmit: async (values) => {
			await toast.promise(createUser.mutateAsync(values), {
				loading: t('common.loading'),
				success: () => {
					formik.resetForm();
					return t('organisation.users.create.success');
				},
				error: t('organisation.users.create.error', {
					error:
						(createUser.error as any)?.message ||
						'No message available',
				}),
			});
			queryClient.invalidateQueries(
				getOrganisationUserListQueryKey(organisation.routeName!)
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
					id='email'
					name='email'
					label='Email'
					value={formik.values.email}
					onChange={formik.handleChange}
					error={formik.touched.email && Boolean(formik.errors.email)}
					helperText={formik.touched.email && formik.errors.email}
					disabled={loading}
				/>
				<Stack direction='row' spacing={2}>
					<TextField
						fullWidth
						id='firstName'
						name='firstName'
						label={t('organisation.users.create.firstName')}
						value={formik.values.firstName}
						onChange={formik.handleChange}
						error={
							formik.touched.firstName &&
							Boolean(formik.errors.firstName)
						}
						helperText={
							formik.touched.firstName && formik.errors.firstName
						}
						disabled={loading}
					/>
					<TextField
						fullWidth
						id='lastName'
						name='lastName'
						label={t('organisation.users.create.lastName')}
						value={formik.values.lastName}
						onChange={formik.handleChange}
						error={
							formik.touched.lastName &&
							Boolean(formik.errors.lastName)
						}
						helperText={
							formik.touched.lastName && formik.errors.lastName
						}
						disabled={loading}
					/>
				</Stack>
				<FormControl fullWidth disabled={loading}>
					<InputLabel id='role-select-label'>
						{t('organisation.users.create.role')}
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
						label={t('organisation.users.create.role')}
					>
						{roles.data?.map((role) => (
							<MenuItem value={role.id} key={role.id}>
								{role.name}
							</MenuItem>
						))}
					</Select>
					<FormHelperText>
						{formik.touched.roleId && formik.errors.roleId}
					</FormHelperText>
				</FormControl>
				<LoadingButton
					type='submit'
					loading={loading}
					variant='contained'
				>
					{t('organisation.users.create.submit')}
				</LoadingButton>
			</Stack>
		</form>
	);
};

export default OrganisationUserForm;
