import LoadingButton from '@mui/lab/LoadingButton/LoadingButton';
import Grid from '@mui/material/Grid';
import TextField from '@mui/material/TextField/TextField';
import { useFormik } from 'formik';
import React from 'react';
import { toast } from 'react-hot-toast';
import { useTranslation } from 'react-i18next';
import { useMutation, useQueryClient } from 'react-query';
import * as yup from 'yup';
import { CreateReleaseRequest, ServiceRecord } from '../../../api/models';
import {
	getReleasesListQueryKey,
	releaseCreate,
} from '../../../api/release-endpoints/release-endpoints';
import { useLocalesList } from '../../../api/service/service';

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
	const locales = useLocalesList(service.routeName!);
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
					return t('release.create.success');
				},
				error: t('release.create.error', {
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
						{t('release.create.submit')}
					</LoadingButton>
				</Grid>
			</Grid>
		</form>
	);
};

export default ReleaseForm;
