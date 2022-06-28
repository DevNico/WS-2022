import React from 'react';
import TextField from '@mui/material/TextField/TextField';
import { Stack } from '@mui/material';
import { useTranslation } from 'react-i18next';
import TabPanel from '../TabPanel';
import { useFormik } from 'formik';
import { RecoilLoadable } from 'recoil';

interface MainTabPanelProps {
	formik: ReturnType<typeof useFormik>;
	index: number;
	value: number;
	loading: boolean;
}

const MainTabPanel: React.FC<MainTabPanelProps> = ({
	formik,
	index,
	value,
	loading,
}) => {
	const { t } = useTranslation();

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
