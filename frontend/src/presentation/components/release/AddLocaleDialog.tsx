import React from 'react';
import Dialog from '@mui/material/Dialog/Dialog';
import DialogTitle from '@mui/material/DialogTitle/DialogTitle';
import DialogContent from '@mui/material/DialogContent/DialogContent';
import {
	Button,
	FormControl,
	InputLabel,
	MenuItem,
	Select,
	SelectChangeEvent,
	Stack,
} from '@mui/material';
import { LocaleRecord } from '../../../api/models';
import { useTranslation } from 'react-i18next';

export interface AddLocaleDialogProps {
	locales: LocaleRecord[];
	open: boolean;
	onClose: (locale?: number) => void;
}

const AddLocaleDialog: React.FC<AddLocaleDialogProps> = ({
	open,
	locales,
	onClose,
}) => {
	const { t } = useTranslation();
	const [locale, setLocale] = React.useState<string | null>(null);

	const handleChange = (event: SelectChangeEvent) => {
		setLocale(event.target.value);
	};

	const handleClose = () => {
		const parsed = Number.parseInt(locale ?? '');
		onClose(isNaN(parsed) ? undefined : parsed);
		setLocale(null);
	};

	return (
		<Dialog open={open} onClose={() => onClose()}>
			<DialogTitle>
				{t('release.create.addLocalizedMetadata')}
			</DialogTitle>
			<DialogContent>
				<Stack
					spacing={2}
					sx={{ margin: '10px 0' }}
					justifyContent='center'
					alignItems='center'
				>
					<FormControl fullWidth>
						<InputLabel id='locale-select-label'>
							{t('release.create.locale')}
						</InputLabel>
						<Select
							labelId='locale-select-label'
							id='locale-select'
							value={locale ?? ''}
							label={t('release.create.locale')}
							onChange={handleChange}
						>
							{locales.map((locale) => (
								<MenuItem key={locale.id} value={locale.id}>
									{locale.tag}
								</MenuItem>
							))}
						</Select>
					</FormControl>
					<Button variant='contained' onClick={handleClose}>
						{t('common.submit')}
					</Button>
				</Stack>
			</DialogContent>
		</Dialog>
	);
};

export default AddLocaleDialog;
