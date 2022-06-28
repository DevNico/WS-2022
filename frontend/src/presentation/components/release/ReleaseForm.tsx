import LoadingButton from '@mui/lab/LoadingButton/LoadingButton';
import { FormikTouched, useFormik } from 'formik';
import React from 'react';
import { toast } from 'react-hot-toast';
import { useTranslation } from 'react-i18next';
import { useMutation, useQueryClient } from 'react-query';
import * as yup from 'yup';
import {
	CreateReleaseRequest,
	LocaleRecord,
	ServiceRecord,
} from '../../../api/models';
import {
	getServicesListReleasesQueryKey,
	useLocalesList,
	useServiceGetServiceTemplate,
} from '../../../api/service/service';
import { Box, Stack, Tab, Tabs } from '@mui/material';
import { releaseCreate } from '../../../api/release/release';
import MainTabPanel from './MainTabPanel';
import Center from '../layout/Center';
import TabPanel from '../TabPanel';
import GeneratedServiceTemplateForm from './GeneratedServiceTemplateForm';
import {
	createYupSchemaFromServiceTemplateMetadata,
	formikDefaultValuesFromServiceTemplateMetadata,
} from '../../../common/serviceTemplateMetadataUtil';
import LocalizedMetadataTabPanel from './LocalizedMetadataTabPanel';

function createLocalesSchema(
	locales: LocaleRecord[],
	data: Record<string, any>
) {
	const res: Record<string, any> = {};
	locales.forEach((locale) => {
		res['locale-' + locale.tag] = data;
	});

	return res;
}

function hasErrors(
	errors?: Record<string, any>,
	touched?: FormikTouched<Record<string, string | boolean>>
): boolean {
	if (!errors || !touched) {
		return false;
	}

	return Object.keys(errors).some((key) => !!errors[key] && touched[key]);
}

interface FormValues {
	serviceId: number;
	version: string;
	staticMetadata: Record<string, string | boolean>;
	[key: string]: Record<string, string | boolean> | number | string;
}

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
	const [currentTab, setCurrentTab] = React.useState(0);
	const [selectedLocales, setSelectedLocales] = React.useState<
		LocaleRecord[]
	>([]);

	const validationSchema = yup.object({
		version: yup.string().min(2).max(50).required(),
		staticMetadata: createYupSchemaFromServiceTemplateMetadata(
			serviceTemplate.data?.staticMetadata ?? []
		),
		...createLocalesSchema(
			selectedLocales,
			createYupSchemaFromServiceTemplateMetadata(
				serviceTemplate.data?.localizedMetadata ?? []
			)
		),
	});

	const formik = useFormik<FormValues>({
		initialValues: {
			serviceId: service.id!,
			version: '',
			staticMetadata: formikDefaultValuesFromServiceTemplateMetadata(
				serviceTemplate.data?.staticMetadata ?? []
			),
			...createLocalesSchema(
				selectedLocales,
				formikDefaultValuesFromServiceTemplateMetadata(
					serviceTemplate.data?.localizedMetadata ?? []
				)
			),
		},
		validationSchema,
		onSubmit: async (values) => {
			const request: CreateReleaseRequest = {
				serviceId: values.serviceId,
				version: values.version,
				metaData: JSON.stringify('values.staticMetadata'),
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

	const handleLocaleAdd = (locale: LocaleRecord) => {
		setSelectedLocales([...selectedLocales, locale]);
	};

	const handleLocaleDelete = (locale: LocaleRecord) => {
		setSelectedLocales(selectedLocales.filter((l) => l.id !== locale.id));
	};

	const basicInfoHasErrors = formik.errors.version && formik.touched.version;
	const staticMetadataHasErrors = hasErrors(
		formik.errors.staticMetadata,
		formik.touched.staticMetadata
	);
	const localizedMetadataHasErrors = Object.keys(formik.errors)
		.filter((k) => k.startsWith('locale'))
		.some((key: string) =>
			hasErrors(formik.errors[key] as any, formik.touched[key] as any)
		);

	return (
		<>
			<form onSubmit={formik.handleSubmit}>
				<Box sx={{ borderBottom: 1, borderColor: 'divider' }}>
					<Tabs
						centered
						value={currentTab}
						onChange={(_, v) => setCurrentTab(v)}
						aria-label='basic tabs example'
					>
						<Tab
							sx={{
								color: basicInfoHasErrors ? 'red' : undefined,
							}}
							label={t('release.create.basicInformation')}
							value={0}
						/>
						<Tab
							sx={{
								color: staticMetadataHasErrors
									? 'red'
									: undefined,
							}}
							label={t('release.create.staticMetadata')}
							value={1}
						/>
						<Tab
							sx={{
								color: localizedMetadataHasErrors
									? 'red'
									: undefined,
							}}
							label={t('release.create.localizedMetadata')}
							value={2}
						/>
					</Tabs>
				</Box>
				<MainTabPanel
					formik={formik as any}
					index={0}
					value={currentTab}
					loading={isLoading}
				/>
				<TabPanel value={currentTab} index={1}>
					<Stack
						spacing={2}
						justifyContent='center'
						alignItems='center'
					>
						<GeneratedServiceTemplateForm
							template={
								serviceTemplate.data?.staticMetadata ?? []
							}
							formik={formik}
							prefix='staticMetadata'
						/>
					</Stack>
				</TabPanel>
				<LocalizedMetadataTabPanel
					formik={formik as any}
					index={2}
					value={currentTab}
					metadata={serviceTemplate.data?.localizedMetadata ?? []}
					loading={isLoading}
					onLocaleAdd={handleLocaleAdd}
					onLocaleDelete={handleLocaleDelete}
					locales={locales.data ?? []}
				/>
				<Center>
					<LoadingButton
						type='submit'
						loading={isLoading}
						variant='contained'
					>
						{t('release.create.submit')}
					</LoadingButton>
				</Center>
			</form>
		</>
	);
};

export default ReleaseForm;
