import Dialog from '@mui/material/Dialog/Dialog';
import DialogContent from '@mui/material/DialogContent/DialogContent';
import DialogTitle from '@mui/material/DialogTitle/DialogTitle';
import React from 'react';
import { useTranslation } from 'react-i18next';
import { ServiceRecord } from '../../../api/models';
import LocaleForm from './LocaleForm';

export interface CreateLocaleDialogProps {
	service: ServiceRecord;
	open: boolean;
	onClose: () => void;
}

const CreateLocaleDialog: React.FC<CreateLocaleDialogProps> = ({
	service,
	open,
	onClose,
}) => {
	const { t } = useTranslation();

	return (
		<Dialog open={open} onClose={onClose}>
			<DialogTitle>{t('locale.create.title')}</DialogTitle>
			<DialogContent>
				<LocaleForm service={service} onSubmitSuccess={onClose} />
			</DialogContent>
		</Dialog>
	);
};

export default CreateLocaleDialog;
