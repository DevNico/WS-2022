import React from 'react';
import { Box, Stack, Tab, Tabs } from '@mui/material';
import MainTabPanel from './MainTabPanel';
import TabPanel from '../TabPanel';
import GeneratedServiceTemplateForm from './GeneratedServiceTemplateForm';
import LocalizedMetadataTabPanel from './LocalizedMetadataTabPanel';
import Center from '../layout/Center';
import LoadingButton from '@mui/lab/LoadingButton/LoadingButton';
import { FormikTouched, useFormikContext } from 'formik';
import { LocaleRecord, MetadataArrayElement } from '../../../api/models';
import { useTranslation } from 'react-i18next';
import { ReleaseFormValues } from './ReleaseForm';

function hasErrors(
	errors?: Record<string, any>,
	touched?: FormikTouched<Record<string, string | boolean>>
): boolean {
	if (!errors || !touched) {
		return false;
	}

	return Object.keys(errors).some((key) => !!errors[key] && touched[key]);
}

interface ReleaseFormContentsProps {
	isLoading: boolean;
	locales: LocaleRecord[];
	localizedMetadata: MetadataArrayElement[];
	staticMetadata: MetadataArrayElement[];
	onLocaleAdd: (locale: LocaleRecord) => void;
	onLocaleDelete: (locale: LocaleRecord) => void;
}

const ReleaseFormContents: React.FC<ReleaseFormContentsProps> = ({
	isLoading,
	locales,
	onLocaleAdd,
	onLocaleDelete,
	localizedMetadata,
	staticMetadata,
}) => {
	const [currentTab, setCurrentTab] = React.useState(0);

	const formik = useFormikContext<ReleaseFormValues>();
	const { t } = useTranslation();

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
							color: staticMetadataHasErrors ? 'red' : undefined,
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
				<Stack spacing={2} justifyContent='center' alignItems='center'>
					<GeneratedServiceTemplateForm
						template={staticMetadata}
						prefix='staticMetadata'
					/>
				</Stack>
			</TabPanel>
			<LocalizedMetadataTabPanel
				index={2}
				value={currentTab}
				metadata={localizedMetadata}
				loading={isLoading}
				onLocaleAdd={onLocaleAdd}
				onLocaleDelete={onLocaleDelete}
				locales={locales}
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
		</>
	);
};

export default ReleaseFormContents;
