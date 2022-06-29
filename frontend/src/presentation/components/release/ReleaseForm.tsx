import { Formik, FormikHelpers } from 'formik';
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
import { releaseCreate } from '../../../api/release/release';
import {
	createYupSchemaFromServiceTemplateMetadata,
	formikDefaultValuesFromServiceTemplateMetadata,
} from '../../../common/serviceTemplateMetadataUtil';
import ReleaseFormContents from './ReleaseFormContents';

export type LocaleFormKey = `locale-${string}`;

export interface ReleaseFormValues {
	serviceId: number;
	version: string;
	staticMetadata: Record<string, string | boolean>;
	[key: LocaleFormKey]: Record<string, string | boolean>;
}

function createLocalesSchema(
	locales: LocaleRecord[],
	data: Record<string, any>
): Record<LocaleFormKey, any> {
	const res: Record<LocaleFormKey, any> = {};
	locales.forEach((locale) => {
		res[`locale-${locale.tag}`] = data;
	});

	return res;
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
	const [selectedLocales, setSelectedLocales] = React.useState<
		LocaleRecord[]
	>([]);

	const isLoading =
		createRelease.isLoading ||
		locales.isLoading ||
		serviceTemplate.isLoading;

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

	const initialValues = {
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
	};

	const onSubmit = async (
		values: ReleaseFormValues,
		formik: FormikHelpers<ReleaseFormValues>
	) => {
		const request: CreateReleaseRequest = {
			serviceId: values.serviceId,
			version: values.version,
			metaData: JSON.stringify(values.staticMetadata),
			localisedMetadataList: selectedLocales.map((locale) => ({
				localeId: locale.id!,
				metadata: JSON.stringify(values[`locale-${locale.tag}`]),
			})),
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
	};

	const handleLocaleAdd = (locale: LocaleRecord) => {
		setSelectedLocales([...selectedLocales, locale]);
	};

	const handleLocaleDelete = (locale: LocaleRecord) => {
		setSelectedLocales(selectedLocales.filter((l) => l.id !== locale.id));
	};

	return (
		<Formik
			initialValues={initialValues}
			onSubmit={onSubmit}
			validationSchema={validationSchema}
		>
			{(formik) => (
				<form onSubmit={formik.handleSubmit}>
					<ReleaseFormContents
						isLoading={isLoading}
						locales={locales.data ?? []}
						localizedMetadata={
							serviceTemplate.data?.localizedMetadata ?? []
						}
						staticMetadata={
							serviceTemplate.data?.staticMetadata ?? []
						}
						onLocaleAdd={handleLocaleAdd}
						onLocaleDelete={handleLocaleDelete}
					/>
				</form>
			)}
		</Formik>
	);
};

export default ReleaseForm;
