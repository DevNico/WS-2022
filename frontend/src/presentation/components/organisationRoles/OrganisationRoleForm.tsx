import LoadingButton from '@mui/lab/LoadingButton/LoadingButton';
import Checkbox from '@mui/material/Checkbox';
import FormControl from '@mui/material/FormControl';
import FormControlLabel from '@mui/material/FormControlLabel';
import Stack from '@mui/material/Stack';
import TextField from '@mui/material/TextField';
import Typography from '@mui/material/Typography';
import { useFormik } from 'formik';
import React from 'react';
import { toast } from 'react-hot-toast';
import { useTranslation } from 'react-i18next';
import { useMutation, useQueryClient } from 'react-query';
import * as yup from 'yup';
import {
	CreateOrganisationRoleRequest,
	OrganisationRecord,
} from '../../../api/models';
import {
	getOrganisationRolesListQueryKey,
	organisationRoleCreate,
} from '../../../api/organisation-role/organisation-role';

export interface OrganisationRoleFormProps {
	organisation: OrganisationRecord;
	onSubmitSuccess: () => void;
}

const OrganisationRoleForm: React.FC<OrganisationRoleFormProps> = ({
	organisation,
	onSubmitSuccess,
}) => {
	const { t } = useTranslation();

	const queryClient = useQueryClient();
	const createRole = useMutation(organisationRoleCreate);
	const loading = createRole.isLoading;

	const validationSchema = yup.object({
		name: yup.string().min(5).max(50).required('Name is required'),
		serviceWrite: yup.boolean().required('Service write is required'),
		serviceDelete: yup.boolean().required('Service delete is required'),
		userRead: yup.boolean().required('User read is required'),
		userWrite: yup.boolean().required('UserWrite is required'),
		userDelete: yup.boolean().required('User delete is required'),
	});

	const formik = useFormik<CreateOrganisationRoleRequest>({
		initialValues: {
			name: '',
			serviceWrite: false,
			serviceDelete: false,
			userRead: false,
			userWrite: false,
			userDelete: false,
			organisationId: organisation.id!,
		},
		validationSchema,
		onSubmit: async (values) => {
			await toast.promise(createRole.mutateAsync(values), {
				loading: t('common.loading'),
				success: () => {
					formik.resetForm();
					return t('organisation.roles.create.success');
				},
				error: t('organisation.roles.create.error', {
					error:
						(createRole.error as any)?.message ||
						'No message available',
				}),
			});
			queryClient.invalidateQueries(
				getOrganisationRolesListQueryKey(organisation.routeName!)
			);
			onSubmitSuccess();
		},
	});

	return (
		<form onSubmit={formik.handleSubmit}>
			<Stack spacing={2} mt={1}>
				<TextField
					fullWidth
					id='name'
					name='name'
					label={t('organisation.roles.create.name')}
					value={formik.values.name}
					onChange={formik.handleChange}
					error={formik.touched.name && Boolean(formik.errors.name)}
					helperText={formik.touched.name && formik.errors.name}
					disabled={loading}
				/>
				<Typography variant='h6'>
					{t('organisation.roles.create.permissions')}
				</Typography>
				<Stack direction='row' spacing={3}>
					<Stack>
						<Typography variant='body2'>
							{t('organisation.roles.create.service')}
						</Typography>
						<FormControlLabel
							disabled={loading}
							control={
								<Checkbox
									id='serviceWrite'
									checked={formik.values.serviceWrite}
									onChange={formik.handleChange}
								/>
							}
							label={t('organisation.roles.create.serviceWrite')}
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
							label={t('organisation.roles.create.serviceDelete')}
						/>
					</Stack>
					<Stack>
						<Typography variant='body2'>
							{t('organisation.roles.create.user')}
						</Typography>
						<FormControlLabel
							disabled={loading}
							control={
								<Checkbox
									id='userRead'
									checked={formik.values.userRead}
									onChange={formik.handleChange}
								/>
							}
							label={t('organisation.roles.create.userRead')}
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
							label={t('organisation.roles.create.userWrite')}
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
							label={t('organisation.roles.create.userDelete')}
						/>
					</Stack>
				</Stack>
				<LoadingButton
					loading={loading}
					type='submit'
					variant='contained'
				>
					{t('common.submit')}
				</LoadingButton>
			</Stack>
		</form>
	);
};
export default OrganisationRoleForm;
