import React from 'react';
import TextField from '@mui/material/TextField/TextField';
import { Stack } from '@mui/material';
import { useTranslation } from 'react-i18next';
import TabPanel from '../TabPanel';
import { useFormikContext } from 'formik';
import { ReleaseFormValues } from './ReleaseForm';

interface MainTabPanelProps {
	index: number;
	value: number;
	loading: boolean;
}

const MainTabPanel: React.FC<MainTabPanelProps> = ({
	index,
	value,
	loading,
}) => {
	const { t } = useTranslation();
	const formik = useFormikContext<ReleaseFormValues>();

	return (
		<TabPanel value={value} index={index}>
			<Stack spacing={2} justifyContent='center' alignItems='center'>
				<TextField
					id='version'
					name='version'
					label={t('release.model.version')}
					value={formik.values.version}
					onChange={formik.handleChange}
					error={
						formik.touched.version && Boolean(formik.errors.version)
					}
					helperText={
						formik.touched.version &&
						(formik.errors.version as string)
					}
					disabled={loading}
				/>
			</Stack>
		</TabPanel>
	);
};

export default MainTabPanel;
