import LoadingButton from '@mui/lab/LoadingButton/LoadingButton';
import TextField from '@mui/material/TextField/TextField';
import { useFormik } from 'formik';
import React from 'react';
import { toast } from 'react-hot-toast';
import { useTranslation } from 'react-i18next';
import { useMutation, useQueryClient } from 'react-query';
import * as yup from 'yup';
import {
	CreateReleaseRequest,
	ServiceRecord,
} from '../../../api/models';
import {
	getServicesListReleasesQueryKey,
	useLocalesList, useServiceGetServiceTemplate
} from "../../../api/service/service";
import GeneratedServiceTemplateForm from './GeneratedServiceTemplateForm';
import {
	Stack,
	Typography,
} from '@mui/material';
import {
	createYupSchemaFromServiceTemplateMetadata,
	formikDefaultValuesFromServiceTemplateMetadata,
} from '../../../common/serviceTemplateMetadataUtil';
import { releaseCreate } from '../../../api/release/release';

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
	const serviceTemplate = useServiceGetServiceTemplate(service.routeName!);
	const isLoading =
		createRelease.isLoading ||
		locales.isLoading ||
		serviceTemplate.isLoading;

	const validationSchema = yup.object({
		version: yup.string().min(2).max(50).required(),
		staticMetadata: createYupSchemaFromServiceTemplateMetadata(
			serviceTemplate.data?.staticMetadata ?? []
		),
	});

	const formik = useFormik({
		initialValues: {
			serviceId: service.id!,
			version: '',
			staticMetadata: formikDefaultValuesFromServiceTemplateMetadata(
				serviceTemplate.data?.staticMetadata ?? []
			),
		},
		validationSchema,
		onSubmit: async (values) => {
			const request: CreateReleaseRequest = {
				serviceId: values.serviceId,
				version: values.version,
				metaData: JSON.stringify(values.staticMetadata),
				localisedMetadataList: [],
			};

			await toast.promise(createRelease.mutateAsync(request), {
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
			await queryClient.invalidateQueries(
				getServicesListReleasesQueryKey(service.id!)
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
				sx={{ marginTop: 2 }}
			>
				<TextField
					fullWidth
					id='version'
					name='version'
					label='Version'
					value={formik.values.version}
					onChange={formik.handleChange}
					error={
						formik.touched.version && Boolean(formik.errors.version)
					}
					helperText={formik.touched.version && formik.errors.version}
					disabled={isLoading}
				/>
				{serviceTemplate.data?.name && (
					<>
						<Typography>
							{t('release.create.staticMetadata')}
						</Typography>
						<GeneratedServiceTemplateForm
							template={serviceTemplate.data?.staticMetadata ?? []}
							prefix="staticMetadata"
							formik={formik}
						/>
					</>
				)}
				<LoadingButton
					type='submit'
					loading={isLoading}
					variant='contained'
				>
					{t('release.create.submit')}
				</LoadingButton>
			</Stack>
		</form>
	);
};

export default ReleaseForm;
