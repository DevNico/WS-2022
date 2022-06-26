import Dialog from '@mui/material/Dialog/Dialog';
import DialogContent from '@mui/material/DialogContent/DialogContent';
import DialogTitle from '@mui/material/DialogTitle/DialogTitle';
import React from 'react';
import { useTranslation } from 'react-i18next';
import { OrganisationRecord } from '../../../api/models';
import ServiceForm from './ServiceForm';

export interface CreateServiceDialogProps {
	organisation: OrganisationRecord;
	open: boolean;
	onClose: () => void;
}

const CreateServiceDialog: React.FC<CreateServiceDialogProps> = ({
	organisation,
	open,
	onClose,
}) => {
	const { t } = useTranslation();

	return (
		<Dialog open={open} onClose={onClose}>
			<DialogTitle>{t('services.create.title')}</DialogTitle>
			<DialogContent>
				<ServiceForm
					onSubmitSuccess={onClose}
					organisation={organisation}
				/>
			</DialogContent>
		</Dialog>
	);
};

export default CreateServiceDialog;
