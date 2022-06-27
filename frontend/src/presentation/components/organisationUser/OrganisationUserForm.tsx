import LoadingButton from '@mui/lab/LoadingButton/LoadingButton';
import Grid from '@mui/material/Grid';
import MenuItem from '@mui/material/MenuItem/MenuItem';
import TextField from '@mui/material/TextField/TextField';
import { useFormik } from 'formik';
import React from 'react';
import { toast } from 'react-hot-toast';
import { useTranslation } from 'react-i18next';
import { useMutation, useQueryClient } from 'react-query';
import * as yup from 'yup';
import {
	CreateOrganisationUserRequest,
	OrganisationRecord,
} from '../../../api/models';
import { useOrganisationRolesList } from '../../../api/organisation-role/organisation-role';
import {
	getOrganisationUserListQueryKey,
	organisationUserCreate,
} from '../../../api/organisation-user/organisation-user';
import OrganisationRoleDetails from '../organisationRole/OrganisationRoleDetails';

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
		roleId: yup.number().positive('Role is required'),
	});

	const formik = useFormik<CreateOrganisationUserRequest>({
		initialValues: {
			organisationId: organisation.id!,
			email: '',
			firstName: '',
			lastName: '',
			roleId: -1,
		},
		validationSchema,
		onSubmit: async (values) => {
			await toast.promise(createUser.mutateAsync(values), {
				loading: t('common.loading'),
				success: () => {
					formik.resetForm();
					return t('organisationUser.create.success');
				},
				error: t('organisationUser.create.error', {
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
			<Grid container spacing={2} justifyContent='center'>
				<Grid item xs={12}>
					<TextField
						fullWidth
						id='email'
						name='email'
						label={t('organisationUser.model.email')}
						value={formik.values.email}
						onChange={formik.handleChange}
						error={
							formik.touched.email && Boolean(formik.errors.email)
						}
						helperText={formik.touched.email && formik.errors.email}
						disabled={loading}
					/>
				</Grid>
				<Grid item sm={6} xs={12}>
					<TextField
						fullWidth
						id='firstName'
						name='firstName'
						label={t('organisationUser.model.firstName')}
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
				</Grid>
				<Grid item sm={6} xs={12}>
					<TextField
						fullWidth
						id='lastName'
						name='lastName'
						label={t('organisationUser.model.lastName')}
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
				</Grid>
				<Grid item xs={12}>
					<TextField
						fullWidth
						select
						id='roleId'
						name='roleId'
						label={t('organisationUser.model.role')}
						value={
							formik.values.roleId === -1
								? ''
								: formik.values.roleId
						}
						onChange={formik.handleChange}
						error={
							formik.touched.roleId &&
							Boolean(formik.errors.roleId)
						}
						helperText={
							formik.touched.roleId && formik.errors.roleId
						}
						disabled={loading}
					>
						{roles.data?.map((role) => (
							<MenuItem value={role.id} key={role.id}>
								{role.name}
							</MenuItem>
						))}
					</TextField>
				</Grid>
				<Grid item xs={12}>
					<OrganisationRoleDetails
						role={
							roles.data?.filter(
								(role) => role.id == formik.values.roleId
							)[0]
						}
					/>
				</Grid>
				<Grid item>
					<LoadingButton
						type='submit'
						loading={loading}
						variant='contained'
					>
						{t('organisationUser.create.submit')}
					</LoadingButton>
				</Grid>
			</Grid>
		</form>
	);
};

export default OrganisationUserForm;
