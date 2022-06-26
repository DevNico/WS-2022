import LoadingButton from '@mui/lab/LoadingButton/LoadingButton';
import Checkbox from '@mui/material/Checkbox';
import FormControlLabel from '@mui/material/FormControlLabel';
import Grid from '@mui/material/Grid';
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
					return t('organisationRole.create.success');
				},
				error: t('organisationRole.create.error', {
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
			<Grid container spacing={2} justifyContent='center'>
				<Grid item xs={12}>
					<TextField
						fullWidth
						id='name'
						name='name'
						label={t('organisationRole.create.name')}
						value={formik.values.name}
						onChange={formik.handleChange}
						error={
							formik.touched.name && Boolean(formik.errors.name)
						}
						helperText={formik.touched.name && formik.errors.name}
						disabled={loading}
					/>
				</Grid>
				<Grid item xs={12}>
					<Typography variant='h6'>
						{t('organisationRole.create.permissions')}
					</Typography>
				</Grid>
				<Grid item sm={6} xs={12}>
					<Stack>
						<Typography variant='body2'>
							{t('organisationRole.create.service')}
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
							label={t('organisationRole.model.serviceWrite')}
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
							label={t('organisationRole.model.serviceDelete')}
						/>
					</Stack>
				</Grid>
				<Grid item sm={6} xs={12}>
					<Stack>
						<Typography variant='body2'>
							{t('organisationRole.create.user')}
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
							label={t('organisationRole.model.userRead')}
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
							label={t('organisationRole.model.userWrite')}
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
							label={t('organisationRole.model.userDelete')}
						/>
					</Stack>
				</Grid>
				<Grid item>
					<LoadingButton
						loading={loading}
						type='submit'
						variant='contained'
					>
						{t('common.submit')}
					</LoadingButton>
				</Grid>
			</Grid>
		</form>
	);
};
export default OrganisationRoleForm;
