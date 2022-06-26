import LoadingButton from '@mui/lab/LoadingButton/LoadingButton';
import Checkbox from '@mui/material/Checkbox';
import FormControlLabel from '@mui/material/FormControlLabel';
import Grid from '@mui/material/Grid';
import MenuItem from '@mui/material/MenuItem';
import TextField from '@mui/material/TextField/TextField';
import { useFormik } from 'formik';
import localeCodes from 'locale-codes';
import React, { useMemo } from 'react';
import toast from 'react-hot-toast';
import { useTranslation } from 'react-i18next';
import { useMutation, useQueryClient } from 'react-query';
import * as yup from 'yup';
import { localeCreate } from '../../../api/locale/locale';
import { CreateLocaleRequest, ServiceRecord } from '../../../api/models';
import { getLocalesListQueryKey } from '../../../api/service/service';

interface CreateLocaleFormProps {
	service: ServiceRecord;
	onSubmitSuccess: () => void;
}

const LocaleForm: React.FC<CreateLocaleFormProps> = ({
	service,
	onSubmitSuccess,
}) => {
	const { t, i18n } = useTranslation();

	const queryClient = useQueryClient();
	const createUser = useMutation(localeCreate);
	const loading = createUser.isLoading;

	const validationSchema = yup.object({
		tag: yup.string().required(),
	});

	const formik = useFormik<CreateLocaleRequest>({
		initialValues: {
			tag: '',
			isDefault: false,
			serviceId: service.id!,
		},
		validationSchema,
		onSubmit: async (values) => {
			await toast.promise(createUser.mutateAsync(values), {
				loading: t('common.loading'),
				success: () => {
					formik.resetForm();
					return t('locale.create.success');
				},
				error: t('locale.create.error', {
					error:
						(createUser.error as any)?.message ||
						'No message available',
				}),
			});
			queryClient.invalidateQueries(
				getLocalesListQueryKey(service.routeName!)
			);
			onSubmitSuccess();
		},
	});

	return (
		<form onSubmit={formik.handleSubmit}>
			<Grid container spacing={2} justifyContent='center' sx={{ pt: 1 }}>
				<Grid item xs={12}>
					<TextField
						fullWidth
						id='tag'
						name='tag'
						label={t('locale.model.language')}
						value={formik.values.tag}
						onChange={formik.handleChange}
						error={formik.touched.tag && Boolean(formik.errors.tag)}
						helperText={formik.touched.tag && formik.errors.tag}
						disabled={loading}
						select
						SelectProps={{ native: true }}
					>
						{localeCodes.all.map((locale) => {
							return (
								<option value={locale.tag} key={locale.tag}>
									{locale.name}{' '}
									{locale.location && `(${locale.location})`}
								</option>
							);
						})}
					</TextField>
				</Grid>
				<Grid item xs={12}>
					<FormControlLabel
						control={
							<Checkbox
								id='isDefault'
								checked={formik.values.isDefault ?? false}
								onChange={formik.handleChange}
							/>
						}
						label={t('locale.model.isDefault')}
					/>
				</Grid>
				<Grid item>
					<LoadingButton
						type='submit'
						loading={loading}
						variant='contained'
					>
						{t('locale.create.submit')}
					</LoadingButton>
				</Grid>
			</Grid>
		</form>
	);
};

export default LocaleForm;
