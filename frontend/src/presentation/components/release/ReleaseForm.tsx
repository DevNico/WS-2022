import LoadingButton from '@mui/lab/LoadingButton/LoadingButton';
import FormControl from '@mui/material/FormControl/FormControl';
import FormHelperText from '@mui/material/FormHelperText/FormHelperText';
import Grid from '@mui/material/Grid';
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
	CreateReleaseRequest,
	OrganisationRecord,
	ReleaseRecord,
	ServiceRecord,
} from '../../../api/models';
import { useOrganisationRolesList } from '../../../api/organisation-role/organisation-role';
import {
	getReleasesListQueryKey,
	releaseCreate,
} from '../../../api/release-endpoints/release-endpoints';
import { useLocalesList } from '../../../api/service/service';
import OrganisationRoleDetails from '../organisationRoles/OrganisationRoleDetails';

interface CreateReleaseFormProps {
	service: ServiceRecord;
	onSubmitSuccess: () => void;
}

const ReleaseForm: React.FC<CreateReleaseFormProps> = ({
	service,
	onSubmitSuccess,
}) => {
	const { t } = useTranslation();

	const queryClient = useQueryClient();
	const createRelease = useMutation(releaseCreate);
	const locales = useLocalesList(service.id!);
	const isLoading = createRelease.isLoading || locales.isLoading;

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

	const formik = useFormik<CreateReleaseRequest>({
		initialValues: {
			serviceId: service.id!,
			version: '',
			metaData: '',
			localisedMetadataList: [],
		},
		validationSchema,
		onSubmit: async (values) => {
			await toast.promise(createRelease.mutateAsync(values), {
				loading: t('common.loading'),
				success: () => {
					formik.resetForm();
					return t('releases.create.success');
				},
				error: t('releases.create.error', {
					error:
						(createRelease.error as any)?.message ||
						'No message available',
				}),
			});
			queryClient.invalidateQueries(getReleasesListQueryKey(service.id!));
			onSubmitSuccess();
		},
	});

	return (
		<form onSubmit={formik.handleSubmit}>
			<Grid container spacing={2} justifyContent='center'>
				<Grid item xs={12}>
					<TextField
						fullWidth
						id='version'
						name='version'
						label='Email'
						value={formik.values.version}
						onChange={formik.handleChange}
						error={
							formik.touched.version &&
							Boolean(formik.errors.version)
						}
						helperText={
							formik.touched.version && formik.errors.version
						}
						disabled={isLoading}
					/>
				</Grid>
				<Grid item>
					<LoadingButton
						type='submit'
						loading={isLoading}
						variant='contained'
					>
						{t('releases.create.submit')}
					</LoadingButton>
				</Grid>
			</Grid>
		</form>
	);
};

export default ReleaseForm;
