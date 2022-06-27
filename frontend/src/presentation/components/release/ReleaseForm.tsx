import LoadingButton from '@mui/lab/LoadingButton/LoadingButton';
import Grid from '@mui/material/Grid';
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
	ServiceTemplateRecord,
} from '../../../api/models';
import {
	getReleasesListQueryKey,
	releaseCreate,
} from '../../../api/release-endpoints/release-endpoints';
import {
	getServicesListReleasesQueryKey,
	useLocalesList,
	useServiceListServiceTemplates,
} from '../../../api/service/service';
import GeneratedServiceTemplateForm from './GeneratedServiceTemplateForm';
import {
	FormControl,
	InputLabel,
	MenuItem,
	Select,
	SelectChangeEvent,
	Stack,
	Typography,
} from '@mui/material';
import {
	createYupSchemaFromServiceTemplateMetadata,
	formikDefaultValuesFromServiceTemplateMetadata,
} from '../../../common/serviceTemplateMetadataUtil';

interface CreateReleaseFormProps {
	service: ServiceRecord;
	onSubmitSuccess: () => void;
}

const ReleaseForm: React.FC<CreateReleaseFormProps> = ({
	service,
	onSubmitSuccess,
}) => {
	const { t } = useTranslation();
	const [selectedTemplate, setSelectedTemplate] =
		React.useState<ServiceTemplateRecord | null>(null);

	const queryClient = useQueryClient();
	const createRelease = useMutation(releaseCreate);
	const locales = useLocalesList(service.routeName!);
	const serviceTemplates = useServiceListServiceTemplates(service.routeName!);
	const isLoading =
		createRelease.isLoading ||
		locales.isLoading ||
		serviceTemplates.isLoading;

	const validationSchema = yup.object({
		version: yup.string().min(2).max(50).required(),
		staticMetadata: createYupSchemaFromServiceTemplateMetadata(
			selectedTemplate?.staticMetadata ?? []
		),
	});

	const formik = useFormik({
		initialValues: {
			serviceId: service.id!,
			version: '',
			staticMetadata: formikDefaultValuesFromServiceTemplateMetadata(
				selectedTemplate?.staticMetadata ?? []
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

	const handleServiceTemplateSelect = (event: SelectChangeEvent) => {
		setSelectedTemplate(
			serviceTemplates.data?.find((t) => t.name === event.target.value) ||
				null
		);
	};

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
				<FormControl fullWidth>
					<InputLabel id='template-select-label'>
						{t('release.create.selectServiceTemplate')}
					</InputLabel>
					<Select
						value={selectedTemplate?.name ?? ''}
						labelId='template-select-label'
						id='template-select'
						label={t('release.create.selectServiceTemplate')}
						disabled={
							isLoading || serviceTemplates.data?.length === 0
						}
						onChange={handleServiceTemplateSelect}
					>
						{serviceTemplates.data?.map((template, index) => (
							<MenuItem key={index} value={template.name!}>
								{template.name}
							</MenuItem>
						))}
					</Select>
				</FormControl>
				{selectedTemplate?.name && (
					<>
						<Typography>
							{t('release.create.staticMetadata')}
						</Typography>
						<GeneratedServiceTemplateForm
							template={selectedTemplate?.staticMetadata ?? []}
							formik={{
								values: formik.values.staticMetadata,
								errors: formik.errors.staticMetadata || {},
								touched: formik.touched.staticMetadata || {},
								handleChange: formik.handleChange,
							}}
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
